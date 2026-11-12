#if NET5_0_OR_GREATER || NETSTANDARD2_0
using Microsoft.Extensions.Logging;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Checkout
{
    /// <summary>
    /// Credentials that obtain an OAuth access token with the client credentials flow and reuse it until it expires.
    /// </summary>
    public sealed class OAuthSdkCredentials : SdkCredentials
    {
#if NET5_0_OR_GREATER || NETSTANDARD2_0
        private readonly ILogger _log = LogProvider.GetLogger(typeof(OAuthSdkCredentials));
#endif
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly JsonSerializer _serializer = new JsonSerializer();
        private readonly ISet<OAuthScope> _scopes;
        private readonly HttpClient _httpClient;
        private readonly Uri _authorizationUri;
        private readonly object _accessTokenLock = new object();
        private volatile OAuthAccessToken _accessToken;

        // Kept as it was: callers and tests match this message.
        private const string TokenRequestFailedMessage =
            "OAuth client_credentials authentication failed with error: invalid_client";

        /// <summary>
        /// Creates OAuth credentials. No token is requested until one is needed.
        /// </summary>
        /// <param name="httpClientFactory">The factory that creates the HTTP client used for token requests.</param>
        /// <param name="authorizationUri">The absolute URI of the token endpoint; must use https, http is accepted for loopback hosts.</param>
        /// <param name="clientId">The OAuth client ID.</param>
        /// <param name="clientSecret">The OAuth client secret.</param>
        /// <param name="scopes">The scopes requested for the token.</param>
        /// <exception cref="CheckoutArgumentException">A parameter is null or blank, or authorizationUri is not absolute or does not use https on a host that is not loopback.</exception>
        public OAuthSdkCredentials(
            IHttpClientFactory httpClientFactory,
            Uri authorizationUri,
            string clientId,
            string clientSecret,
            ISet<OAuthScope> scopes) : base(PlatformType.DefaultOAuth)
        {
            CheckoutUtils.ValidateParams(
                "httpClientFactory", httpClientFactory,
                "authorizationUri", authorizationUri,
                "clientId", clientId,
                "clientSecret", clientSecret,
                "scopes", scopes);
            ValidateAuthorizationUri(authorizationUri);
            _clientId = clientId;
            _clientSecret = clientSecret;
            _scopes = scopes;
            _authorizationUri = authorizationUri;
            // The factory may hand out a client the merchant shares with their own code, so the
            // token request uses an absolute URI and leaves BaseAddress alone. With the SDK's own
            // factory the token client does not follow redirects: a 307 or 308 would resend the
            // form, client_secret included, to the Location host.
            _httpClient = httpClientFactory is DefaultHttpClientFactory
                ? new HttpClient(new HttpClientHandler { AllowAutoRedirect = false })
                : httpClientFactory.CreateClient();
        }

        internal static void ValidateAuthorizationUri(Uri authorizationUri)
        {
            CheckoutUtils.ValidateHttpsUri("authorizationUri", authorizationUri);
        }

        internal void InitAccess()
        {
            GetAccessToken();
        }

        /// <summary>
        /// Returns an authorization that carries a valid access token, requesting a new token when there is none or it has expired.
        /// </summary>
        /// <param name="authorizationType">The authorization type required by the operation.</param>
        /// <returns>The authorization with the current access token.</returns>
        /// <exception cref="CheckoutAuthorizationException">The authorization type is not SecretKeyOrOAuth, PublicKeyOrOAuth or OAuth, or the token request fails or returns an invalid response.</exception>
        public override SdkAuthorization GetSdkAuthorization(SdkAuthorizationType authorizationType)
        {
            switch (authorizationType)
            {
                case SdkAuthorizationType.SecretKeyOrOAuth:
                case SdkAuthorizationType.PublicKeyOrOAuth:
                case SdkAuthorizationType.OAuth:
                    return new SdkAuthorization(PlatformType, GetAccessToken().Token);
                default:
                    throw CheckoutAuthorizationException.InvalidAuthorization(authorizationType);
            }
        }

        private OAuthAccessToken GetAccessToken()
        {
            var accessToken = _accessToken;
            if (accessToken != null && accessToken.IsValid())
            {
                return accessToken;
            }

            lock (_accessTokenLock)
            {
                if (_accessToken != null && _accessToken.IsValid())
                {
                    return _accessToken;
                }

                var response = Request();
                try
                {
                    _accessToken = OAuthAccessToken.FromOAuthServiceResponse(response);
                }
                catch (Exception e) when (e is ArgumentException || e is OverflowException)
                {
                    // An out-of-range expires_in overflows the expiry date.
                    throw new CheckoutAuthorizationException(TokenRequestFailedMessage, e);
                }

                return _accessToken;
            }
        }

        private OAuthServiceResponse Request()
        {
#if NET5_0_OR_GREATER || NETSTANDARD2_0
            _log.LogInformation("requesting OAuth token using client_credentials flow");
#endif
            try
            {
                var data = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", _clientId),
                    new KeyValuePair<string, string>("client_secret", _clientSecret),
                    new KeyValuePair<string, string>("scope", GetScopes())
                };
                using (var content = new FormUrlEncodedContent(data))
                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, _authorizationUri) { Content = content })
                // GetSdkAuthorization is synchronous, so this is a blocking wait. ConfigureAwait(false)
                // only keeps the continuation off the caller's context; it does not make the wait safe
                // on a single-threaded synchronization context.
                using (var responseMessage =
                       _httpClient.SendAsync(httpRequest).ConfigureAwait(false).GetAwaiter().GetResult())
                {
                    // A merchant client may follow redirects; a token from another host is refused.
                    var finalUri = responseMessage.RequestMessage?.RequestUri;
                    if (!responseMessage.IsSuccessStatusCode || (finalUri != null &&
                        (finalUri.Scheme != _authorizationUri.Scheme || finalUri.Port != _authorizationUri.Port ||
                         !string.Equals(finalUri.Host, _authorizationUri.Host, StringComparison.OrdinalIgnoreCase))))
                    {
                        throw new CheckoutAuthorizationException(TokenRequestFailedMessage);
                    }

                    var body = responseMessage.Content == null
                        ? null
                        : responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                    OAuthServiceResponse oAuthServiceResponse = null;
                    try
                    {
                        oAuthServiceResponse = string.IsNullOrWhiteSpace(body)
                            ? null
                            : (OAuthServiceResponse)_serializer.Deserialize(body, typeof(OAuthServiceResponse));
                    }
                    catch (Exception)
                    {
                        // The body holds the access token; the parser exception can quote it.
                    }

                    if (oAuthServiceResponse == null || !oAuthServiceResponse.IsValid())
                    {
                        throw new CheckoutAuthorizationException(TokenRequestFailedMessage);
                    }

                    // An unusable token is refused here so it is never cached.
                    try
                    {
                        CheckoutUtils.ValidateHeaderValue("Authorization", "Bearer " + oAuthServiceResponse.AccessToken);
                    }
                    catch (CheckoutArgumentException)
                    {
                        throw new CheckoutAuthorizationException(TokenRequestFailedMessage);
                    }

                    return oAuthServiceResponse;
                }
            }
            catch (CheckoutAuthorizationException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new CheckoutAuthorizationException(TokenRequestFailedMessage, e);
            }
        }

        private string GetScopes()
        {
            return string.Join(" ",
                _scopes.Select(scope => scope.GetAttribute<OAuthScopeAttribute>().Scope).ToArray());
        }
    }
}
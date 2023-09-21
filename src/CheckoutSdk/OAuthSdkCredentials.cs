#if (NETSTANDARD2_0_OR_GREATER || NETCOREAPP3_1_OR_GREATER)
using Microsoft.Extensions.Logging;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Checkout
{
    public sealed class OAuthSdkCredentials : SdkCredentials
    {
#if (NETSTANDARD2_0_OR_GREATER || NETCOREAPP3_1_OR_GREATER)
        private readonly ILogger _log = LogProvider.GetLogger(typeof(OAuthSdkCredentials));
#endif

        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly JsonSerializer _serializer = new JsonSerializer();
        private readonly ISet<OAuthScope> _scopes;
        private readonly HttpClient _httpClient;
        private OAuthAccessToken _accessToken;

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
            _clientId = clientId;
            _clientSecret = clientSecret;
            _scopes = scopes;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = authorizationUri;
        }

        internal void InitAccess()
        {
            GetAccessToken();
        }

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
            if (_accessToken != null && _accessToken.IsValid())
            {
                return _accessToken;
            }

            _accessToken = OAuthAccessToken.FromOAuthServiceResponse(Request());
            return _accessToken;
        }

        private OAuthServiceResponse Request()
        {
#if (NETSTANDARD2_0_OR_GREATER || NETCOREAPP3_1_OR_GREATER)
            _log.LogInformation("requesting OAuth token using client_credentials flow");
#endif
            try
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Post, string.Empty);
                var data = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("client_id", _clientId),
                    new KeyValuePair<string, string>("client_secret", _clientSecret),
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("scope", GetScopes())
                };
                httpRequest.Content = new FormUrlEncodedContent(data);
                var responseMessage = _httpClient.SendAsync(httpRequest).GetAwaiter().GetResult();
                var oAuthServiceResponse = (OAuthServiceResponse)_serializer.Deserialize(
                    responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult(),
                    typeof(OAuthServiceResponse));
                if (!oAuthServiceResponse.IsValid())
                {
                    throw new ArgumentException("Invalid OAuth client authorization response");
                }

                return oAuthServiceResponse;
            }
            catch (Exception e)
            {
                throw new CheckoutAuthorizationException(
                    "OAuth client_credentials authentication failed with error: invalid_client", e);
            }
        }

        private string GetScopes()
        {
            return string.Join(" ",
                _scopes.Select(scope => scope.GetAttribute<OAuthScopeAttribute>().Scope).ToArray());
        }
    }
}
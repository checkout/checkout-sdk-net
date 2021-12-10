using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace Checkout.Four
{
    public sealed class FourOAuthSdkCredentials : SdkCredentials
    {
        private readonly ILogger _log = LogProvider.GetLogger(typeof(FourOAuthSdkCredentials));

        private readonly Uri _authorizationUri;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly JsonSerializer _serializer = new JsonSerializer();
        private readonly ISet<FourOAuthScope> _scopes;
        private readonly HttpClient _httpClient;
        private OAuthAccessToken _accessToken;

        public FourOAuthSdkCredentials(
            IHttpClientFactory httpClientFactory,
            Uri authorizationUri,
            string clientId,
            string clientSecret,
            ISet<FourOAuthScope> scopes) : base(PlatformType.FourOAuth)
        {
            CheckoutUtils.ValidateParams(
                "httpClientFactory", httpClientFactory,
                "authorizationUri", authorizationUri,
                "clientId", clientId,
                "clientSecret", clientSecret,
                "scopes", scopes);

            _authorizationUri = authorizationUri;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _scopes = scopes;
            _httpClient = httpClientFactory.CreateClient();
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
            _log.LogInformation("requesting OAuth token using client_credentials flow");
            try
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Post, _authorizationUri);
                var data = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("client_id", _clientId),
                    new KeyValuePair<string, string>("client_secret", _clientSecret),
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("scope", GetScopes())
                };
                httpRequest.Content = new FormUrlEncodedContent(data);
                var responseMessage = _httpClient.SendAsync(httpRequest).GetAwaiter().GetResult();
                var oAuthServiceResponse = (OAuthServiceResponse) _serializer.Deserialize(
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
            return string.Join(' ',
                _scopes.Select(scope => scope.GetAttribute<FourOAuthScopeAttribute>().Scope).ToArray());
        }
    }
}
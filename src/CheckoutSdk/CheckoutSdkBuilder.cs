using Checkout.Previous;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Checkout
{
    public class CheckoutSdkBuilder
    {
        public CheckoutPreviousSdk Previous()
        {
            return new CheckoutPreviousSdk();
        }
        public CheckoutStaticKeysSdkBuilder StaticKeys()
        {
            return new CheckoutStaticKeysSdkBuilder();
        }

        public CheckoutOAuthSdkBuilder OAuth()
        {
            return new CheckoutOAuthSdkBuilder();
        }

        public class CheckoutStaticKeysSdkBuilder : AbstractCheckoutSdkBuilder<ICheckoutApi>
        {
            private string _publicKey;
            private string _secretKey;

            public CheckoutStaticKeysSdkBuilder PublicKey(string publicKey)
            {
                _publicKey = publicKey;
                return this;
            }

            public CheckoutStaticKeysSdkBuilder SecretKey(string secretKey)
            {
                _secretKey = secretKey;
                return this;
            }

            protected override SdkCredentials GetSdkCredentials()
            {
                return new StaticKeysSdkCredentials(_secretKey, _publicKey);
            }

            public override ICheckoutApi Build()
            {
                return new CheckoutApi(GetCheckoutConfiguration());
            }
        }

        public class CheckoutOAuthSdkBuilder : AbstractCheckoutSdkBuilder<ICheckoutApi>
        {
            private string _clientId;
            private string _clientSecret;
            private Uri _authorizationUri;
            private readonly ISet<OAuthScope> _scopes = new HashSet<OAuthScope>();

            public CheckoutOAuthSdkBuilder ClientCredentials(string clientId, string clientSecret)
            {
                _clientId = clientId;
                _clientSecret = clientSecret;
                return this;
            }

            public CheckoutOAuthSdkBuilder AuthorizationUri(Uri authorizationUri)
            {
                _authorizationUri = authorizationUri;
                return this;
            }
            
            public CheckoutOAuthSdkBuilder Scopes(params OAuthScope[] scopes)
            {
                CheckoutUtils.ValidateParams("scopes", scopes);
                foreach (var scope in scopes)
                {
                    _scopes.Add(scope);
                }

                return this;
            }

            protected override SdkCredentials GetSdkCredentials()
            {
                if (_authorizationUri == null)
                {
                    _authorizationUri = Env.GetAttribute<EnvironmentAttribute>().AuthorizationUri;
                }

                var credentials = new OAuthSdkCredentials(
                    ClientFactory,
                    Client, 
                    _authorizationUri, 
                    _clientId,
                    _clientSecret,
                    _scopes);

                credentials.InitAccess();
                return credentials;
            }

            public override ICheckoutApi Build()
            {
                return new CheckoutApi(GetCheckoutConfiguration());
            }
        }
    }
}
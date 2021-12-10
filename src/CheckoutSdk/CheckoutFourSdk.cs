using System;
using System.Collections.Generic;
using Checkout.Four;

namespace Checkout
{
    public class CheckoutFourSdk
    {
        public FourStaticKeysCheckoutSdkBuilder StaticKeys()
        {
            return new FourStaticKeysCheckoutSdkBuilder();
        }

        public FourOAuthCheckoutSdkBuilder OAuth()
        {
            return new FourOAuthCheckoutSdkBuilder();
        }

        public class FourStaticKeysCheckoutSdkBuilder : AbstractCheckoutSdkBuilder<Four.ICheckoutApi>
        {
            private string _publicKey;
            private string _secretKey;

            public FourStaticKeysCheckoutSdkBuilder PublicKey(string publicKey)
            {
                _publicKey = publicKey;
                return this;
            }

            public FourStaticKeysCheckoutSdkBuilder SecretKey(string secretKey)
            {
                _secretKey = secretKey;
                return this;
            }

            protected override SdkCredentials GetSdkCredentials()
            {
                return new FourStaticKeysSdkCredentials(_secretKey, _publicKey);
            }

            public override Four.ICheckoutApi Build()
            {
                return new Four.CheckoutApi(GetCheckoutConfiguration());
            }
        }

        public class FourOAuthCheckoutSdkBuilder : AbstractCheckoutSdkBuilder<Four.ICheckoutApi>
        {
            private string _clientId;
            private string _clientSecret;
            private Uri _authorizationUri;
            private readonly ISet<FourOAuthScope> _scopes = new HashSet<FourOAuthScope>();

            public FourOAuthCheckoutSdkBuilder ClientCredentials(string clientId, string clientSecret)
            {
                _clientId = clientId;
                _clientSecret = clientSecret;
                return this;
            }

            public FourOAuthCheckoutSdkBuilder AuthorizationUri(Uri authorizationUri)
            {
                _authorizationUri = authorizationUri;
                return this;
            }

            public FourOAuthCheckoutSdkBuilder Scopes(params FourOAuthScope[] scopes)
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

                var credentials = new FourOAuthSdkCredentials(ClientFactory, _authorizationUri, _clientId,
                    _clientSecret,
                    _scopes);

                credentials.InitAccess();
                return credentials;
            }

            public override Four.ICheckoutApi Build()
            {
                return new Four.CheckoutApi(GetCheckoutConfiguration());
            }
        }
    }
}
using Checkout.Previous;
using System;
using System.Collections.Generic;

namespace Checkout
{
    /// <summary>
    /// Entry point to build a Checkout.com API client.
    /// </summary>
    public class CheckoutSdkBuilder
    {
        /// <summary>
        /// Returns the entry point to build a client for the previous platform.
        /// </summary>
        /// <returns>A new previous platform SDK entry point.</returns>
        public CheckoutPreviousSdk Previous()
        {
            return new CheckoutPreviousSdk();
        }
        /// <summary>
        /// Returns a builder for a client authenticated with a secret key and an optional public key.
        /// </summary>
        /// <returns>A new static keys builder.</returns>
        public CheckoutStaticKeysSdkBuilder StaticKeys()
        {
            return new CheckoutStaticKeysSdkBuilder();
        }

        /// <summary>
        /// Returns a builder for a client authenticated with OAuth client credentials.
        /// </summary>
        /// <returns>A new OAuth builder.</returns>
        public CheckoutOAuthSdkBuilder OAuth()
        {
            return new CheckoutOAuthSdkBuilder();
        }

        /// <summary>
        /// Builds a client authenticated with static API keys.
        /// </summary>
        public class CheckoutStaticKeysSdkBuilder : AbstractCheckoutSdkBuilder<ICheckoutApi>
        {
            private string _publicKey;
            private string _secretKey;

            /// <summary>
            /// Sets the public key.
            /// </summary>
            /// <param name="publicKey">The public key.</param>
            /// <returns>This builder.</returns>
            public CheckoutStaticKeysSdkBuilder PublicKey(string publicKey)
            {
                _publicKey = publicKey;
                return this;
            }

            /// <summary>
            /// Sets the secret key.
            /// </summary>
            /// <param name="secretKey">The secret key.</param>
            /// <returns>This builder.</returns>
            public CheckoutStaticKeysSdkBuilder SecretKey(string secretKey)
            {
                _secretKey = secretKey;
                return this;
            }

            protected override SdkCredentials GetSdkCredentials()
            {
                return new StaticKeysSdkCredentials(_secretKey, _publicKey);
            }

            /// <summary>
            /// Builds the API client with the configured keys and environment.
            /// </summary>
            /// <returns>The API client.</returns>
            public override ICheckoutApi Build()
            {
                return new CheckoutApi(GetCheckoutConfiguration());
            }
        }

        /// <summary>
        /// Builds a client authenticated with OAuth client credentials.
        /// </summary>
        public class CheckoutOAuthSdkBuilder : AbstractCheckoutSdkBuilder<ICheckoutApi>
        {
            private string _clientId;
            private string _clientSecret;
            private Uri _authorizationUri;
            private readonly ISet<OAuthScope> _scopes = new HashSet<OAuthScope>();

            /// <summary>
            /// Sets the OAuth client credentials.
            /// </summary>
            /// <param name="clientId">The OAuth client ID.</param>
            /// <param name="clientSecret">The OAuth client secret.</param>
            /// <returns>This builder.</returns>
            public CheckoutOAuthSdkBuilder ClientCredentials(string clientId, string clientSecret)
            {
                _clientId = clientId;
                _clientSecret = clientSecret;
                return this;
            }

            /// <summary>
            /// Sets a custom token endpoint. When not set, the endpoint is derived from the environment subdomain or the environment.
            /// </summary>
            /// <param name="authorizationUri">The absolute URI of the token endpoint; must use https, http is accepted for loopback hosts. Null clears it.</param>
            /// <returns>This builder.</returns>
            /// <exception cref="CheckoutArgumentException">The URI is not absolute or does not use https on a host that is not loopback.</exception>
            public CheckoutOAuthSdkBuilder AuthorizationUri(Uri authorizationUri)
            {
                OAuthSdkCredentials.ValidateAuthorizationUri(authorizationUri);
                _authorizationUri = authorizationUri;
                return this;
            }

            /// <summary>
            /// Adds scopes to request for the access token.
            /// </summary>
            /// <param name="scopes">The scopes to add.</param>
            /// <returns>This builder.</returns>
            /// <exception cref="CheckoutArgumentException">scopes is null.</exception>
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
                // Option A of the MSSD review: an explicit authorization URI used to win
                // silently over the subdomain, producing a half-legacy client. The two are
                // now mutually exclusive; a custom token host requires the legacy opt-out.
                if (_authorizationUri != null && GetEnvironmentSubdomain() != null)
                {
                    throw new CheckoutArgumentException(
                        "AuthorizationUri and EnvironmentSubdomain cannot both be set - the token " +
                        "endpoint is derived from your subdomain. Combine AuthorizationUri with " +
                        "UseLegacyDomain() if you need a custom token host.");
                }

                // Resolved locally on every call: caching it in _authorizationUri froze the
                // first Build()'s host, so a reused builder with a new subdomain split the
                // auth and api hosts.
                var authorizationUri = _authorizationUri;
                if (authorizationUri == null)
                {
                    var envSubdomain = GetEnvironmentSubdomain();
                    authorizationUri = envSubdomain != null ?
                                        envSubdomain.AuthorizationUri
                                        : Env.GetAttribute<EnvironmentAttribute>().AuthorizationUri;
                }

                var credentials = new OAuthSdkCredentials(ClientFactory, authorizationUri, _clientId,
                    _clientSecret,
                    _scopes);

                credentials.InitAccess();
                return credentials;
            }

            /// <summary>
            /// Builds the API client and requests the first access token. AuthorizationUri and an environment subdomain cannot both be set; combine AuthorizationUri with UseLegacyDomain for a custom token host.
            /// </summary>
            /// <returns>The API client.</returns>
            /// <exception cref="CheckoutArgumentException">Both AuthorizationUri and an environment subdomain are set, or a credential parameter is null or blank.</exception>
            /// <exception cref="CheckoutAuthorizationException">The access token request fails or returns an invalid response.</exception>
            public override ICheckoutApi Build()
            {
                return new CheckoutApi(GetCheckoutConfiguration());
            }
        }
    }
}
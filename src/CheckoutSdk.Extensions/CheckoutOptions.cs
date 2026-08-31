using Checkout;
using System;
using Environment = Checkout.Environment;

namespace CheckoutSDK.Extensions.Configuration
{
    /// <summary>
    /// Configuration-file counterpart of the SDK builders: each property maps to a
    /// builder option and is bound from the Checkout configuration section.
    /// </summary>
    public class CheckoutOptions
    {
        /// <summary>
        /// The secret key, for the static-keys authorization type.
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// The public key, for the static-keys authorization type.
        /// </summary>
        public string PublicKey { get; set; }

        /// <summary>
        /// The OAuth client id, for the OAuth authorization type.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// The OAuth client secret, for the OAuth authorization type.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// An explicit OAuth token endpoint. When unset, the endpoint is derived from
        /// Environment and EnvironmentSubdomain.
        /// </summary>
        public Uri AuthorizationUri { get; set; }

        /// <summary>
        /// The OAuth scopes to request.
        /// </summary>
        public OAuthScope[] Scopes { get; set; }

        /// <summary>
        /// The Checkout.com environment (Sandbox or Production).
        /// </summary>
        public Environment Environment { get; set; }

        /// <summary>
        /// The merchant-specific subdomain, typically the client ID excluding the cli_
        /// prefix. Required unless UseLegacyDomain is set.
        /// See https://api-reference.checkout.com/#section/Base-URLs
        /// </summary>
        public string EnvironmentSubdomain { get; set; }

        /// <summary>
        /// Emergency opt-out that keeps the SDK on the shared checkout.com hosts.
        /// </summary>
        [Obsolete("UseLegacyDomain is deprecated and will be removed in a future release. It is intended only as an " +
                  "emergency fallback when the merchant-specific subdomain cannot be used. Set EnvironmentSubdomain " +
                  "instead. See https://api-reference.checkout.com/#section/Base-URLs")]
        public bool UseLegacyDomain { get; set; }

        /// <summary>
        /// The platform the credentials belong to; inferred from the keys when unset.
        /// </summary>
        public PlatformType? PlatformType { get; set; }

        /// <summary>
        /// A custom HTTP client factory.
        /// </summary>
        public IHttpClientFactory HttpClientFactory { get; set; }

        /// <summary>
        /// Whether the SDK sends telemetry; enabled by default.
        /// </summary>
        public bool RecordTelemetry { get; set; } = true;
    }
}

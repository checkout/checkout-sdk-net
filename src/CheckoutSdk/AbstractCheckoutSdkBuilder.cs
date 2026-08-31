using System;
#if NET5_0_OR_GREATER || NETSTANDARD2_0
using Microsoft.Extensions.Logging;
#endif

namespace Checkout
{
    public abstract class AbstractCheckoutSdkBuilder<T>
    {
        protected Environment Env = Checkout.Environment.Sandbox;

        private bool _recordTelemetry = true;
        private string _subdomain;
        private bool _useLegacyDomain;
        protected IHttpClientFactory ClientFactory = new DefaultHttpClientFactory();

        public AbstractCheckoutSdkBuilder<T> Environment(Environment environment)
        {
            Env = environment;
            return this;
        }

        /// <summary>
        /// Sets the merchant-specific subdomain, typically your client ID excluding the
        /// cli_ prefix (Private Link merchants keep their pl- prefix). Required unless
        /// UseLegacyDomain() is called. See https://api-reference.checkout.com/#section/Base-URLs
        /// </summary>
        public AbstractCheckoutSdkBuilder<T> EnvironmentSubdomain(string subdomain)
        {
            _subdomain = subdomain;
            return this;
        }

        [Obsolete("UseLegacyDomain is deprecated and will be removed in a future release. It is intended only as an " +
                  "emergency fallback when the merchant-specific subdomain cannot be used. Set EnvironmentSubdomain " +
                  "instead. See https://api-reference.checkout.com/#section/Base-URLs")]
        public AbstractCheckoutSdkBuilder<T> UseLegacyDomain()
        {
            _useLegacyDomain = true;
            return this;
        }

        public AbstractCheckoutSdkBuilder<T> RecordTelemetry(bool recordTelemetry)
        {
            _recordTelemetry = recordTelemetry;
            return this;
        }

#if NET5_0_OR_GREATER || NETSTANDARD2_0
        public AbstractCheckoutSdkBuilder<T> LogProvider(ILoggerFactory loggerFactory)
        {
            Checkout.LogProvider.SetLogFactory(loggerFactory);
            return this;
        }
#endif

        public AbstractCheckoutSdkBuilder<T> HttpClientFactory(IHttpClientFactory httpClientFactory)
        {
            ClientFactory = httpClientFactory;
            return this;
        }

        protected virtual bool RequiresEnvironmentSubdomain => true;

        protected EnvironmentSubdomain GetEnvironmentSubdomain()
        {
            return _subdomain != null ? new EnvironmentSubdomain(Env, _subdomain) : null;
        }

        protected CheckoutConfiguration GetCheckoutConfiguration()
        {
            ValidateEnvironmentSettings();
            return new CheckoutConfiguration(GetSdkCredentials(), Env, GetEnvironmentSubdomain(), ClientFactory,
                _recordTelemetry);
        }

        private void ValidateEnvironmentSettings()
        {
            if (_subdomain != null && _useLegacyDomain)
            {
                throw new CheckoutArgumentException(
                    "EnvironmentSubdomain and UseLegacyDomain cannot both be set - provide only your " +
                    "merchant-specific subdomain");
            }

            if (_subdomain == null && !_useLegacyDomain && RequiresEnvironmentSubdomain)
            {
                throw new CheckoutArgumentException(
                    "EnvironmentSubdomain is required - provide your merchant-specific subdomain (typically " +
                    "your client ID excluding the cli_ prefix, see https://api-reference.checkout.com/#section/Base-URLs), " +
                    "or call UseLegacyDomain() to opt out only if merchant specific sub domains are causing issues");
            }
        }

        protected abstract SdkCredentials GetSdkCredentials();

        public abstract T Build();
    }
}

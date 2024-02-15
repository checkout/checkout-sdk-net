using System;
using System.Text.RegularExpressions;
#if (NETSTANDARD2_0_OR_GREATER || NETCOREAPP3_1_OR_GREATER)
using Microsoft.Extensions.Logging;
#endif

namespace Checkout
{
    public abstract class AbstractCheckoutSdkBuilder<T>
    {
        protected Environment Env = Checkout.Environment.Sandbox;
        private EnvironmentSubdomain _envSubdomain;
        protected IHttpClientFactory ClientFactory = new DefaultHttpClientFactory();

        public AbstractCheckoutSdkBuilder<T> Environment(Environment environment)
        {
            Env = environment;
            return this;
        }

        public AbstractCheckoutSdkBuilder<T> EnvironmentSubdomain(string subdomain)
        {
            _envSubdomain = new EnvironmentSubdomain(Env, subdomain);
            return this;
        }

#if (NETSTANDARD2_0_OR_GREATER || NETCOREAPP3_1_OR_GREATER)
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

        protected CheckoutConfiguration GetCheckoutConfiguration()
        {
            return new CheckoutConfiguration(GetSdkCredentials(), Env, _envSubdomain, ClientFactory);
        }

        protected abstract SdkCredentials GetSdkCredentials();

        public abstract T Build();
    }
}
using Microsoft.Extensions.Logging;
using System;

namespace Checkout
{
    public abstract class AbstractCheckoutSdkBuilder<T>
    {
        protected Environment Env = Checkout.Environment.Sandbox;
        private Environment? _filesEnv;
        protected IHttpClientFactory ClientFactory = new DefaultHttpClientFactory();

        public AbstractCheckoutSdkBuilder<T> Environment(Environment environment)
        {
            Env = environment;
            return this;
        }

        [Obsolete ("Won't be supported anymore from version 6.0.0 in favor of using defined URI's in Environment")]
        public AbstractCheckoutSdkBuilder<T> FilesEnvironment(Environment? environment)
        {
            _filesEnv = environment;
            return this;
        }

        public AbstractCheckoutSdkBuilder<T> LogProvider(ILoggerFactory loggerFactory)
        {
            Checkout.LogProvider.SetLogFactory(loggerFactory);
            return this;
        }

        public AbstractCheckoutSdkBuilder<T> HttpClientFactory(IHttpClientFactory httpClientFactory)
        {
            ClientFactory = httpClientFactory;
            return this;
        }

        protected CheckoutConfiguration GetCheckoutConfiguration()
        {
            return new CheckoutConfiguration(GetSdkCredentials(), Env, ClientFactory, _filesEnv);
        }

        protected abstract SdkCredentials GetSdkCredentials();

        public abstract T Build();
    }
}
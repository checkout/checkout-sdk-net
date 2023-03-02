using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;

namespace Checkout
{
    public abstract class AbstractCheckoutSdkBuilder<T>
    {
        protected Environment Env = Checkout.Environment.Sandbox;
        protected IHttpClientFactory ClientFactory = new DefaultHttpClientFactory();
        protected HttpClient Client = new HttpClient();

        public AbstractCheckoutSdkBuilder<T> Environment(Environment environment)
        {
            Env = environment;
            return this;
        }

        public AbstractCheckoutSdkBuilder<T> LogProvider(ILoggerFactory loggerFactory)
        {
            Checkout.LogProvider.SetLogFactory(loggerFactory);
            return this;
        }

        [Obsolete("This property will be removed in the future, instead use HttpClient", false)]
        public AbstractCheckoutSdkBuilder<T> HttpClientFactory(IHttpClientFactory httpClientFactory)
        {
            ClientFactory = httpClientFactory;
            return this;
        }
        
        public AbstractCheckoutSdkBuilder<T> HttpClient(HttpClient httpClient)
        {
            Client = httpClient;
            return this;
        }

        protected CheckoutConfiguration GetCheckoutConfiguration()
        {
            return new CheckoutConfiguration(GetSdkCredentials(), Env, ClientFactory, Client);
        }

        protected abstract SdkCredentials GetSdkCredentials();

        public abstract T Build();
    }
}
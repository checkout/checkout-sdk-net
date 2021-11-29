using Microsoft.Extensions.Logging;

namespace Checkout
{
    public abstract class AbstractCheckoutSdkBuilder<T>
    {
        private Environment _environment;
        
        private IHttpClientFactory _httpClientFactory = new DefaultHttpClientFactory();

        public AbstractCheckoutSdkBuilder<T> Environment(Environment environment)
        {
            _environment = environment;
            return this;
        }

        public AbstractCheckoutSdkBuilder<T> LogProvider(ILoggerFactory loggerFactory)
        {
            Checkout.LogProvider.SetLogFactory(loggerFactory);
            return this;
        }
        
        public AbstractCheckoutSdkBuilder<T> HttpClientFactory(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            return this;
        }

        protected CheckoutConfiguration GetCheckoutConfiguration()
        {
            return new CheckoutConfiguration(GetSdkCredentials(), _environment, _httpClientFactory);
        }

        protected abstract SdkCredentials GetSdkCredentials();

        public abstract T Build();
    }
}
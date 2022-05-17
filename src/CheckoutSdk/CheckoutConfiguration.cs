namespace Checkout
{
    public class CheckoutConfiguration
    {
        public SdkCredentials SdkCredentials { get; }

        public Environment Environment { get; }

        public IHttpClientFactory HttpClientFactory { get; }

        public CheckoutConfiguration(
            SdkCredentials sdkCredentials,
            Environment environment,
            IHttpClientFactory httpClientFactory)
        {
            CheckoutUtils.ValidateParams("sdkCredentials", sdkCredentials, "environment", environment,
                "httpClientFactory", httpClientFactory);
            SdkCredentials = sdkCredentials;
            Environment = environment;
            HttpClientFactory = httpClientFactory;
        }
    }
}
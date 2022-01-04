namespace Checkout
{
    public class CheckoutConfiguration : ICheckoutConfiguration
    {
        public SdkCredentials SdkCredentials { get; private set; }

        public Environment Environment { get; private set; }

        public IHttpClientFactory HttpClientFactory { get; private set; }

        private CheckoutFilesConfiguration FilesApiConfiguration { get; set; }

        public CheckoutConfiguration(
            SdkCredentials sdkCredentials,
            Environment environment,
            IHttpClientFactory httpClientFactory)
        {
            Init(sdkCredentials, environment, httpClientFactory);
            FilesApiConfiguration = new CheckoutFilesConfiguration(environment);
        }

        public CheckoutConfiguration(SdkCredentials sdkCredentials, Environment environment,
            IHttpClientFactory httpClientFactory, CheckoutFilesConfiguration filesApiConfiguration)
        {
            Init(sdkCredentials, environment, httpClientFactory);
            FilesApiConfiguration = filesApiConfiguration;
        }

        public CheckoutFilesConfiguration GetFilesApiConfiguration()
        {
            return FilesApiConfiguration;
        }

        private void Init(
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
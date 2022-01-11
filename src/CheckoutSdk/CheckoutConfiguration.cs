namespace Checkout
{
    public class CheckoutConfiguration
    {
        public SdkCredentials SdkCredentials { get; }

        public Environment Environment { get; }

        public IHttpClientFactory HttpClientFactory { get; }

        public CheckoutFilesConfiguration FilesApiConfiguration { get; }

        public CheckoutConfiguration(
            SdkCredentials sdkCredentials,
            Environment environment,
            IHttpClientFactory httpClientFactory,
            Environment? filesEnvironment = null)
        {
            CheckoutUtils.ValidateParams("sdkCredentials", sdkCredentials, "environment", environment,
                "httpClientFactory", httpClientFactory);
            SdkCredentials = sdkCredentials;
            Environment = environment;
            HttpClientFactory = httpClientFactory;
            if (filesEnvironment != null)
            {
                FilesApiConfiguration = new CheckoutFilesConfiguration(filesEnvironment);
            }
        }
    }
}
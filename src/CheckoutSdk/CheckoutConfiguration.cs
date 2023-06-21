using System.Net.Http;

namespace Checkout
{
    public class CheckoutConfiguration
    {
        public SdkCredentials SdkCredentials { get; }

        public Environment Environment { get; }

        public HttpClient HttpClient { get; }

        public CheckoutConfiguration(
            SdkCredentials sdkCredentials,
            Environment environment,
            HttpClient httpClient)
        {
            CheckoutUtils.ValidateParams("sdkCredentials", sdkCredentials, "environment", environment,
                "httpClient", httpClient);
            SdkCredentials = sdkCredentials;
            Environment = environment;
            HttpClient = httpClient;
        }
    }
}
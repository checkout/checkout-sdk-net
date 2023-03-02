#nullable enable
using System.Net.Http;

namespace Checkout
{
    public class CheckoutConfiguration
    {
        public SdkCredentials SdkCredentials { get; }
        public Environment Environment { get; }
        public IHttpClientFactory? HttpClientFactory { get; }
        public HttpClient? HttpClient { get; }
        
        public CheckoutConfiguration(
            SdkCredentials sdkCredentials,
            Environment environment,
            IHttpClientFactory? httpClientFactory,
            HttpClient? httpClient)
        {
            CheckoutUtils.ValidateParams("sdkCredentials", sdkCredentials, "environment", environment);
            SdkCredentials = sdkCredentials;
            Environment = environment;
            HttpClientFactory = httpClientFactory;
            HttpClient = httpClient;
        }
    }
}
using Checkout;

namespace CheckoutSDK.Extensions.Configuration
{
    public sealed class CheckoutOptions
    {
        public string SecretKey { get; set; }

        public string PublicKey { get; set; }

        public Environment Environment { get; set; }

        public PlatformType? PlatformType { get; set; }

        public IHttpClientFactory HttpClientFactory { get; set; }
    }
}
using Checkout;

namespace Microsoft.Extensions.Configuration
{
    public class CheckoutOptions
    {
        public string SecretKey { get; set; }
        public string PublicKey { get; set; }
        public bool? Sandbox { get; set; }
        public string Uri { get; set; }

        public CheckoutConfiguration CreateConfiguration()
        {
            var checkoutConfiguration = string.IsNullOrWhiteSpace(Uri)
                ? new CheckoutConfiguration(SecretKey, Sandbox ?? true)
                : new CheckoutConfiguration(SecretKey, Uri);

            checkoutConfiguration.PublicKey = PublicKey;
            return checkoutConfiguration;
        }
    }
}
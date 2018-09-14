namespace Checkout.Microsoft.Extensions
{
    public class CheckoutOptions
    {
        public string SecretKey { get; set; }
        public string PublicKey { get; set; }
        public bool Sandbox { get; set; }
        public string Uri { get; set; }

        public CheckoutConfiguration CreateConfiguration()
        {
            var checkoutConfiguration = string.IsNullOrEmpty(Uri)
                ? new CheckoutConfiguration(SecretKey, Sandbox)
                : new CheckoutConfiguration(SecretKey, Uri);

            checkoutConfiguration.PublicKey = PublicKey;
            return checkoutConfiguration;
        }
    }
}
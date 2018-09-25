using Checkout;

namespace Microsoft.Extensions.Configuration
{
    public class CheckoutOptions
    {
        public string SecretKey { get; set; }
        public string PublicKey { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether to connect to the Checkout Sandbox. 
        /// </summary>
        public bool? UseSandbox { get; set; }
        public string Uri { get; set; }

        public CheckoutConfiguration CreateConfiguration()
        {
            var checkoutConfiguration = string.IsNullOrWhiteSpace(Uri)
                ? new CheckoutConfiguration(SecretKey, UseSandbox ?? true)
                : new CheckoutConfiguration(SecretKey, Uri);

            checkoutConfiguration.PublicKey = PublicKey;
            return checkoutConfiguration;
        }
    }
}
using Checkout;

namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// Defines the options needed to configure the Checkout.com SDK for .NET that can be initialized using the Microsoft configuration framework.
    /// </summary>
    public class CheckoutOptions
    {
        /// <summary>
        /// Gets or sets your Checkout Secret Key.
        /// </summary>
        /// <value></value>
        public string SecretKey { get; set; }
        
        /// <summary>
        /// Gets or sets your Checkout Public Key.
        /// </summary>
        public string PublicKey { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether to connect to the Checkout Sandbox. 
        /// </summary>
        public bool? UseSandbox { get; set; }

        /// <summary>
        /// Gets or sets the API endpoint to connect to.
        /// </summary>
        public string Uri { get; set; }

        
        /// <summary>
        /// Creates a <see cref="Checkout.CheckoutConfiguration"/> needed to configure the SDK.
        /// </summary>
        /// <returns>The initializes configuration.</returns>
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
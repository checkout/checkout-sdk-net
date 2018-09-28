namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// Checkout SDK extensions for <see cref="Microsoft.Extensions.Configuration.IConfiguration"/>.
    /// </summary>
    public static class CheckoutConfigurationExtensions
    {
        /// <summary>
        /// Gets the options from the "Checkout" configuration section needed to configure the Checkout.com SDK for .NET.
        /// </summary>
        /// <param name="configuration">The configuration properties.</param>
        /// <returns>The Checkout options initialized with values from the provided configuration.</returns>
        public static CheckoutOptions GetCheckoutOptions(this IConfiguration configuration)
        {
            return configuration.GetSection("Checkout").Get<CheckoutOptions>();
        }
    }
}
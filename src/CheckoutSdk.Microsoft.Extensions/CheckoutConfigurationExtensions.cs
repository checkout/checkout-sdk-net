namespace Microsoft.Extensions.Configuration
{
    public static class CheckoutConfigurationExtensions
    {
        public static CheckoutOptions GetCheckoutOptions(this IConfiguration configuration)
        {
            return configuration.GetSection("Checkout").Get<CheckoutOptions>();
        }
    }
}
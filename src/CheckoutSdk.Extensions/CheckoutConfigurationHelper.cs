using Microsoft.Extensions.Configuration;

namespace CheckoutSDK.Extensions.Configuration
{
    public static class CheckoutConfigurationHelper
    {
        public static CheckoutOptions GetCheckoutOptions(this IConfiguration configuration)
        {
            return configuration.GetSection("Checkout").Get<CheckoutOptions>();
        }
    }
}
using Microsoft.Extensions.Configuration;

namespace CheckoutSDK.Extensions.Configuration
{
    public static class CheckoutConfigurationHelper
    {
        public static CheckoutOptions GetCheckoutOptions(this IConfiguration configuration)
            => GetCheckoutOptions(configuration.GetSection("Checkout"));

        public static CheckoutOptions GetCheckoutOptions(this IConfigurationSection configurationSection)
            => configurationSection.Get<CheckoutOptions>();
    }
}
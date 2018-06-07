using Microsoft.Extensions.DependencyInjection;
using NSpec;
using Shouldly;

namespace Checkout.Tests
{
    class describe_microsoft_extensions : nspec
    {
        void it_can_resolve_checkout_api()
        {
            var services = new ServiceCollection();
            var configuration = new CheckoutConfiguration("sk_xxx", true);
            
            services.AddCheckoutSdk(configuration);

            var serviceProvider = services.BuildServiceProvider();
            
            var checkoutApi = serviceProvider.GetService<ICheckoutApi>();
            checkoutApi.ShouldNotBeNull();

            var ckoConfig = serviceProvider.GetService<CheckoutConfiguration>();
            ckoConfig.ShouldBeSameAs(configuration);
        }
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace Checkout.Tests
{
    public class MicrosoftExtensionsTests
    {
        [Fact]
        public void CanResolveCheckoutApi()
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
using CheckoutSDK.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using Shouldly;
using Xunit;

namespace Checkout.Extensions
{
    public class CheckoutConfigurationHelperTest
    {
        [Fact]
        private void ShouldGetDefaultAppSettings()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("./Resources/AppSettingsDefaultTest.json")
                .Build();
            var checkoutOptions = configuration.GetCheckoutOptions();
            checkoutOptions.ShouldNotBeNull();
            checkoutOptions.Environment.ShouldBe(Environment.Sandbox);
            checkoutOptions.PlatformType.ShouldBe(PlatformType.Default);
            checkoutOptions.PublicKey.ShouldNotBeNull();
            checkoutOptions.SecretKey.ShouldNotBeNull();
        }

        [Fact]
        private void ShouldGetFourAppSettings()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("./Resources/AppSettingsFourTest.json")
                .Build();
            var checkoutOptions = configuration.GetCheckoutOptions();
            checkoutOptions.ShouldNotBeNull();
            checkoutOptions.Environment.ShouldBe(Environment.Sandbox);
            checkoutOptions.PlatformType.ShouldBe(PlatformType.Four);
            checkoutOptions.PublicKey.ShouldNotBeNull();
            checkoutOptions.SecretKey.ShouldNotBeNull();
        }
    }
}
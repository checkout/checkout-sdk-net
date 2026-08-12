using CheckoutSDK.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using Shouldly;
using Xunit;

namespace Checkout.Extensions
{
    public class CheckoutConfigurationHelperTest
    {
        [Fact]
        private void ShouldGetPreviousAppSettings()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("./Resources/AppSettingsPreviousTest.json")
                .Build();
            var checkoutOptions = configuration.GetCheckoutOptions();
            checkoutOptions.ShouldNotBeNull();
            checkoutOptions.Environment.ShouldBe(Environment.Sandbox);
            checkoutOptions.PlatformType.ShouldBe(PlatformType.Previous);
            checkoutOptions.PublicKey.ShouldNotBeNull();
            checkoutOptions.SecretKey.ShouldNotBeNull();
        }

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
            checkoutOptions.EnvironmentSubdomain.ShouldBe("1234doma");
#pragma warning disable CS0618 // asserting the deprecated option is bound, not recommending it
            checkoutOptions.UseLegacyDomain.ShouldBeFalse();
#pragma warning restore CS0618
        }

        [Fact]
        private void ShouldGetDefaultOAuthAppSettingsWithLegacyDomain()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("./Resources/AppSettingsDefaultOAuthTest.json")
                .Build();
            var checkoutOptions = configuration.GetCheckoutOptions();
            checkoutOptions.ShouldNotBeNull();
            checkoutOptions.PlatformType.ShouldBe(PlatformType.DefaultOAuth);
            checkoutOptions.EnvironmentSubdomain.ShouldBeNull();
#pragma warning disable CS0618 // asserting the deprecated option is bound, not recommending it
            checkoutOptions.UseLegacyDomain.ShouldBeTrue();
#pragma warning restore CS0618
        }

        [Fact]
        private void ShouldApplyTheSubdomainFromAppSettingsToTheBaseUrl()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("./Resources/AppSettingsDefaultTest.json")
                .Build();
            var checkoutOptions = configuration.GetCheckoutOptions();

            var subdomain = new EnvironmentSubdomain(checkoutOptions.Environment,
                checkoutOptions.EnvironmentSubdomain);

            subdomain.ApiUri.ToString().ShouldBe("https://1234doma.api.sandbox.checkout.com/");
            subdomain.AuthorizationUri.ToString()
                .ShouldBe("https://1234doma.access.sandbox.checkout.com/connect/token");
        }
    }
}
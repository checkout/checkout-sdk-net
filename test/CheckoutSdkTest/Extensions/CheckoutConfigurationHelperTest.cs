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
        private void ShouldHonourTheTestSubdomainSwitch()
        {
            var originalSwitch = System.Environment.GetEnvironmentVariable("CHECKOUT_TEST_USE_SUBDOMAIN");
            var originalSubdomain = System.Environment.GetEnvironmentVariable("CHECKOUT_MERCHANT_SUBDOMAIN");
            try
            {
                // A subdomain the SDK rejects, so taking the subdomain path is observable without
                // reaching the network.
                System.Environment.SetEnvironmentVariable("CHECKOUT_MERCHANT_SUBDOMAIN", "NOT VALID");

                System.Environment.SetEnvironmentVariable("CHECKOUT_TEST_USE_SUBDOMAIN", "false");
                TestDomainConfiguration.UseSubdomain.ShouldBeFalse();
                BuildSandboxApi().ShouldNotBeNull();

                System.Environment.SetEnvironmentVariable("CHECKOUT_TEST_USE_SUBDOMAIN", "true");
                TestDomainConfiguration.UseSubdomain.ShouldBeTrue();
                var exception = Assert.Throws<CheckoutArgumentException>(() => BuildSandboxApi());
                exception.Message.ShouldContain("invalid environment subdomain");
            }
            finally
            {
                System.Environment.SetEnvironmentVariable("CHECKOUT_TEST_USE_SUBDOMAIN", originalSwitch);
                System.Environment.SetEnvironmentVariable("CHECKOUT_MERCHANT_SUBDOMAIN", originalSubdomain);
            }
        }

        private static ICheckoutApi BuildSandboxApi()
        {
            return CheckoutSdk.Builder().StaticKeys()
                .SecretKey("sk_sbox_m73dzbpy7cf3gfd46xr4yj5xo4e")
                .Environment(Environment.Sandbox)
                .ConfigureDomain()
                .Build();
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
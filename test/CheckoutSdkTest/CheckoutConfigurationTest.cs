using Moq;
using Shouldly;
using System;
using Xunit;

namespace Checkout
{
    public class CheckoutConfigurationTest : UnitTestFixture
    {
        [Fact]
        private void ShouldCreateConfiguration()
        {
            var credentials = new StaticKeysSdkCredentials(ValidDefaultSk, ValidDefaultPk);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var configuration =
                new CheckoutConfiguration(credentials, Environment.Production, null, httpClientFactoryMock.Object, false);
            configuration.Environment.ShouldBe(Environment.Production);
            configuration.SdkCredentials.ShouldBeAssignableTo(typeof(StaticKeysSdkCredentials));
        }

        [Theory]
        [InlineData("a", "https://a.api.sandbox.checkout.com/")]
        [InlineData("ab", "https://ab.api.sandbox.checkout.com/")]
        [InlineData("abc", "https://abc.api.sandbox.checkout.com/")]
        [InlineData("abc1", "https://abc1.api.sandbox.checkout.com/")]
        [InlineData("12345domain", "https://12345domain.api.sandbox.checkout.com/")]
        public void ShouldCreateConfigurationWithSubdomain(string subdomain, string expectedUri)
        {
            var credentials = new StaticKeysSdkCredentials(ValidDefaultSk, ValidDefaultPk);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var environmentSubdomain = new EnvironmentSubdomain(Environment.Sandbox, subdomain);
            var configuration = new CheckoutConfiguration(credentials, Environment.Sandbox, environmentSubdomain,
                httpClientFactoryMock.Object, false);

            configuration.Environment.ShouldBe(Environment.Sandbox);
            configuration.EnvironmentSubdomain.ApiUri.ToString().ShouldBe(expectedUri);
            configuration.SdkCredentials.ShouldBeAssignableTo(typeof(StaticKeysSdkCredentials));
        }

        [Theory]
        [InlineData("", "https://api.sandbox.checkout.com/")]
        [InlineData(" ", "https://api.sandbox.checkout.com/")]
        [InlineData("  ", "https://api.sandbox.checkout.com/")]
        [InlineData(" - ", "https://api.sandbox.checkout.com/")]
        [InlineData("a b", "https://api.sandbox.checkout.com/")]
        [InlineData("ab c1", "https://api.sandbox.checkout.com/")]
        public void ShouldCreateConfigurationWithBadSubdomain(string subdomain, string expectedUri)
        {
            var credentials = new StaticKeysSdkCredentials(ValidDefaultSk, ValidDefaultPk);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var environmentSubdomain = new EnvironmentSubdomain(Environment.Sandbox, subdomain);
            var configuration = new CheckoutConfiguration(credentials, Environment.Sandbox, environmentSubdomain,
                httpClientFactoryMock.Object, false);

            configuration.Environment.ShouldBe(Environment.Sandbox);
            configuration.EnvironmentSubdomain.ApiUri.ToString().ShouldBe(expectedUri);
            configuration.SdkCredentials.ShouldBeAssignableTo(typeof(StaticKeysSdkCredentials));
        }
    }
}
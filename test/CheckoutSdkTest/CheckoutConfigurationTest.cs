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
                new CheckoutConfiguration(credentials, Environment.Production, null, httpClientFactoryMock.Object);
            configuration.Environment.ShouldBe(Environment.Production);
            configuration.SdkCredentials.ShouldBeAssignableTo(typeof(StaticKeysSdkCredentials));
        }

        [Theory]
        [InlineData("123dmain", "https://123dmain.api.sandbox.checkout.com/")]
        [InlineData("123domain", "https://123domain.api.sandbox.checkout.com/")]
        [InlineData("1234domain", "https://1234domain.api.sandbox.checkout.com/")]
        [InlineData("12345domain", "https://12345domain.api.sandbox.checkout.com/")]
        public void ShouldCreateConfigurationWithSubdomain(string subdomain, string expectedUri)
        {
            var credentials = new StaticKeysSdkCredentials(ValidDefaultSk, ValidDefaultPk);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var environmentSubdomain = new EnvironmentSubdomain(Environment.Sandbox, subdomain);
            var configuration = new CheckoutConfiguration(credentials, Environment.Sandbox, environmentSubdomain,
                httpClientFactoryMock.Object);

            configuration.Environment.ShouldBe(Environment.Sandbox);
            configuration.EnvironmentSubdomain.ApiUri.ToString().ShouldBe(expectedUri);
            configuration.SdkCredentials.ShouldBeAssignableTo(typeof(StaticKeysSdkCredentials));
        }

        [Theory]
        [InlineData("", "https://api.sandbox.checkout.com/")]
        [InlineData("123", "https://api.sandbox.checkout.com/")]
        [InlineData("123bad", "https://api.sandbox.checkout.com/")]
        [InlineData("12345domainBad", "https://api.sandbox.checkout.com/")]
        public void ShouldCreateConfigurationWithBadSubdomain(string subdomain, string expectedUri)
        {
            var credentials = new StaticKeysSdkCredentials(ValidDefaultSk, ValidDefaultPk);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var environmentSubdomain = new EnvironmentSubdomain(Environment.Sandbox, subdomain);
            var configuration = new CheckoutConfiguration(credentials, Environment.Sandbox, environmentSubdomain,
                httpClientFactoryMock.Object);

            configuration.Environment.ShouldBe(Environment.Sandbox);
            configuration.EnvironmentSubdomain.ApiUri.ToString().ShouldBe(expectedUri);
            configuration.SdkCredentials.ShouldBeAssignableTo(typeof(StaticKeysSdkCredentials));
        }
    }
}
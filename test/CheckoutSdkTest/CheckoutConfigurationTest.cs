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
        
        [Fact]
        private void ShouldCreateConfigurationWithSubdomain()
        {
            var credentials = new StaticKeysSdkCredentials(ValidDefaultSk, ValidDefaultPk);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var environmentSubdomain = new EnvironmentSubdomain(Environment.Production, "123dmain");
            var configuration =
                new CheckoutConfiguration(credentials, Environment.Production, environmentSubdomain, httpClientFactoryMock.Object);
            configuration.Environment.ShouldBe(Environment.Production);
            configuration.EnvironmentSubdomain.ApiUri.ToString().ShouldBe("https://123dmain.api.checkout.com/");
            configuration.SdkCredentials.ShouldBeAssignableTo(typeof(StaticKeysSdkCredentials));
        }
        
        [Fact]
        private void ShouldCreateConfigurationWithBadSubdomain()
        {
            var credentials = new StaticKeysSdkCredentials(ValidDefaultSk, ValidDefaultPk);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var environmentSubdomain = new EnvironmentSubdomain(Environment.Production, "123bad");
            var configuration =
                new CheckoutConfiguration(credentials, Environment.Production, environmentSubdomain, httpClientFactoryMock.Object);
            configuration.Environment.ShouldBe(Environment.Production);
            configuration.EnvironmentSubdomain.ApiUri.ToString().ShouldBe("https://api.checkout.com/");
            configuration.SdkCredentials.ShouldBeAssignableTo(typeof(StaticKeysSdkCredentials));
        }
    }
}
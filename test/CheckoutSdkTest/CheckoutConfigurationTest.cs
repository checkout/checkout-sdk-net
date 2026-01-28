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
        [InlineData("a", "https://a.api.sandbox.checkout.com/", "https://a.access.sandbox.checkout.com/connect/token")]
        [InlineData("ab", "https://ab.api.sandbox.checkout.com/", "https://ab.access.sandbox.checkout.com/connect/token")]
        [InlineData("abc", "https://abc.api.sandbox.checkout.com/", "https://abc.access.sandbox.checkout.com/connect/token")]
        [InlineData("abc1", "https://abc1.api.sandbox.checkout.com/", "https://abc1.access.sandbox.checkout.com/connect/token")]
        [InlineData("12345domain", "https://12345domain.api.sandbox.checkout.com/", "https://12345domain.access.sandbox.checkout.com/connect/token")]
        [InlineData("1234doma", "https://1234doma.api.sandbox.checkout.com/", "https://1234doma.access.sandbox.checkout.com/connect/token")]
        public void ShouldCreateConfigurationWithSubdomain(string subdomain, string expectedApiUri, string expectedAuthUri)
        {
            var credentials = new StaticKeysSdkCredentials(ValidDefaultSk, ValidDefaultPk);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var environmentSubdomain = new EnvironmentSubdomain(Environment.Sandbox, subdomain);
            var configuration = new CheckoutConfiguration(credentials, Environment.Sandbox, environmentSubdomain,
                httpClientFactoryMock.Object, false);

            configuration.Environment.ShouldBe(Environment.Sandbox);
            configuration.EnvironmentSubdomain.ApiUri.ToString().ShouldBe(expectedApiUri);
            configuration.EnvironmentSubdomain.AuthorizationUri.ToString().ShouldBe(expectedAuthUri);
            configuration.SdkCredentials.ShouldBeAssignableTo(typeof(StaticKeysSdkCredentials));
        }

        [Theory]
        [InlineData("", "https://api.sandbox.checkout.com/", "https://access.sandbox.checkout.com/connect/token")]
        [InlineData(" ", "https://api.sandbox.checkout.com/", "https://access.sandbox.checkout.com/connect/token")]
        [InlineData("  ", "https://api.sandbox.checkout.com/", "https://access.sandbox.checkout.com/connect/token")]
        [InlineData(" - ", "https://api.sandbox.checkout.com/", "https://access.sandbox.checkout.com/connect/token")]
        [InlineData("a b", "https://api.sandbox.checkout.com/", "https://access.sandbox.checkout.com/connect/token")]
        [InlineData("ab c1", "https://api.sandbox.checkout.com/", "https://access.sandbox.checkout.com/connect/token")]
        public void ShouldCreateConfigurationWithBadSubdomain(string subdomain, string expectedApiUri, string expectedAuthUri)
        {
            var credentials = new StaticKeysSdkCredentials(ValidDefaultSk, ValidDefaultPk);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var environmentSubdomain = new EnvironmentSubdomain(Environment.Sandbox, subdomain);
            var configuration = new CheckoutConfiguration(credentials, Environment.Sandbox, environmentSubdomain,
                httpClientFactoryMock.Object, false);

            configuration.Environment.ShouldBe(Environment.Sandbox);
            configuration.EnvironmentSubdomain.ApiUri.ToString().ShouldBe(expectedApiUri);
            configuration.EnvironmentSubdomain.AuthorizationUri.ToString().ShouldBe(expectedAuthUri);
            configuration.SdkCredentials.ShouldBeAssignableTo(typeof(StaticKeysSdkCredentials));
        }
        
        [Theory]
        [InlineData("1234prod", "https://1234prod.api.checkout.com/", "https://1234prod.access.checkout.com/connect/token")]
        [InlineData("prodcompany", "https://prodcompany.api.checkout.com/", "https://prodcompany.access.checkout.com/connect/token")]
        public void ShouldCreateConfigurationWithSubdomainForProduction(string subdomain, string expectedApiUri, string expectedAuthUri)
        {
            var credentials = new StaticKeysSdkCredentials(ValidDefaultSk, ValidDefaultPk);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var environmentSubdomain = new EnvironmentSubdomain(Environment.Production, subdomain);
            var configuration = new CheckoutConfiguration(credentials, Environment.Production, environmentSubdomain,
                httpClientFactoryMock.Object, false);

            configuration.Environment.ShouldBe(Environment.Production);
            configuration.EnvironmentSubdomain.ApiUri.ToString().ShouldBe(expectedApiUri);
            configuration.EnvironmentSubdomain.AuthorizationUri.ToString().ShouldBe(expectedAuthUri);
            configuration.SdkCredentials.ShouldBeAssignableTo(typeof(StaticKeysSdkCredentials));
        }
    }
}
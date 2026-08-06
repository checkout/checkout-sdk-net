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
        [InlineData("pl-vkuhvk4v", "https://pl-vkuhvk4v.api.sandbox.checkout.com/", "https://pl-vkuhvk4v.access.sandbox.checkout.com/connect/token")]
        [InlineData("pl-abc123", "https://pl-abc123.api.sandbox.checkout.com/", "https://pl-abc123.access.sandbox.checkout.com/connect/token")]
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
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData(" - ")]
        [InlineData("a b")]
        [InlineData("ab c1")]
        [InlineData("foo-")]
        [InlineData("-foo")]
        [InlineData("ABC123")]
        [InlineData("test-123")]
        [InlineData("foo-bar")]
        [InlineData("pl-")]
        public void ShouldThrowForBadSubdomain(string subdomain)
        {
            var exception = Assert.Throws<CheckoutArgumentException>(
                () => new EnvironmentSubdomain(Environment.Sandbox, subdomain));

            exception.Message.ShouldContain("invalid environment subdomain");
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

        [Fact]
        public void ShouldHaveCorrectForwardAndIdentityUrisForSandbox()
        {
            var attr = Environment.Sandbox.GetAttribute<EnvironmentAttribute>();
            attr.ForwardApiUri.ToString().ShouldBe("https://forward.sandbox.checkout.com/");
            attr.IdentityApiUri.ToString().ShouldBe("https://identity-verification.sandbox.checkout.com/");
        }

        [Fact]
        public void ShouldHaveCorrectForwardAndIdentityUrisForProduction()
        {
            var attr = Environment.Production.GetAttribute<EnvironmentAttribute>();
            attr.ForwardApiUri.ToString().ShouldBe("https://forward.checkout.com/");
            attr.IdentityApiUri.ToString().ShouldBe("https://identity-verification.checkout.com/");
        }
    }
}
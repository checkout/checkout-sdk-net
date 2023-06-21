using Moq;
using Shouldly;
using System.Net.Http;
using Xunit;

namespace Checkout
{
    public class CheckoutConfigurationTest : UnitTestFixture
    {
        [Fact]
        private void ShouldCreateConfiguration()
        {
            var credentials = new StaticKeysSdkCredentials(ValidDefaultSk, ValidDefaultPk);
            var httpClientMock = new Mock<HttpClient>();
            var configuration =
                new CheckoutConfiguration(credentials, Environment.Production, httpClientMock.Object);
            configuration.Environment.ShouldBe(Environment.Production);
            configuration.SdkCredentials.ShouldBeAssignableTo(typeof(StaticKeysSdkCredentials));
        }
    }
}
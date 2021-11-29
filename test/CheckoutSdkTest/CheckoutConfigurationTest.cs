using Checkout.Four;
using Moq;
using Shouldly;
using Xunit;

namespace Checkout
{
    public class CheckoutConfigurationTest : UnitTestFixture
    {
        [Fact]
        private void ShouldCreateConfiguration()
        {
            var credentials = new FourStaticKeysSdkCredentials(ValidFourSk, ValidFourPk);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var configuration =
                new CheckoutConfiguration(credentials, Environment.Production, httpClientFactoryMock.Object);
            configuration.Environment.ShouldBe(Environment.Production);
            configuration.SdkCredentials.ShouldBeAssignableTo(typeof(FourStaticKeysSdkCredentials));
        }
    }
}
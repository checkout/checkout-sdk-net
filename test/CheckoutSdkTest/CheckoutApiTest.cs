using System.Net.Http;
using Moq;
using Shouldly;
using Xunit;

namespace Checkout
{
    public class CheckoutApiTest : UnitTestFixture
    {
        [Fact]
        public void ShouldInstantiateAndRetrieveClientsDefault()
        {
            var sdkCredentialsMock = new Mock<SdkCredentials>(MockBehavior.Strict, PlatformType.Default);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(mock => mock.CreateClient())
                .Returns(new HttpClient());
            var checkoutConfiguration = new CheckoutConfiguration(sdkCredentialsMock.Object, Environment.Sandbox,
                httpClientFactoryMock.Object);
            var checkoutApi = new CheckoutApi(checkoutConfiguration);
            checkoutApi.TokensClient().ShouldNotBeNull();
            checkoutApi.CustomersClient().ShouldNotBeNull();
            checkoutApi.SourcesClient().ShouldNotBeNull();
            checkoutApi.PaymentsClient().ShouldNotBeNull();
            checkoutApi.InstrumentsClient().ShouldNotBeNull();
            checkoutApi.WebhooksClient().ShouldNotBeNull();
            checkoutApi.EventsClient().ShouldNotBeNull();
            checkoutApi.DisputesClient().ShouldNotBeNull();
            checkoutApi.RiskClient().ShouldNotBeNull();
            checkoutApi.IdealClient().ShouldNotBeNull();
            checkoutApi.KlarnaClient().ShouldNotBeNull();
            checkoutApi.SepaClient().ShouldNotBeNull();
        }

        [Fact]
        public void ShouldInstantiateAndRetrieveClientsFour()
        {
            var sdkCredentialsMock = new Mock<SdkCredentials>(MockBehavior.Strict, PlatformType.Default);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(mock => mock.CreateClient())
                .Returns(new HttpClient());
            var checkoutConfiguration = new CheckoutConfiguration(sdkCredentialsMock.Object, Environment.Sandbox,
                httpClientFactoryMock.Object);
            var checkoutApi = new Four.CheckoutApi(checkoutConfiguration);
            checkoutApi.TokensClient().ShouldNotBeNull();
            checkoutApi.CustomersClient().ShouldNotBeNull();
            checkoutApi.PaymentsClient().ShouldNotBeNull();
            checkoutApi.InstrumentsClient().ShouldNotBeNull();
            checkoutApi.DisputesClient().ShouldNotBeNull();
            checkoutApi.RiskClient().ShouldNotBeNull();
        }
    }
}
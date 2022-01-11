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
            //Arrange
            var sdkCredentialsMock = new Mock<SdkCredentials>(MockBehavior.Strict, PlatformType.Default);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(mock => mock.CreateClient())
                .Returns(new HttpClient());
            var checkoutConfiguration = new CheckoutConfiguration(sdkCredentialsMock.Object, Environment.Sandbox,
                httpClientFactoryMock.Object);

            //Act
            var checkoutApi = new CheckoutApi(checkoutConfiguration);

            //Assert
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
            checkoutApi.ReconciliationClient().ShouldNotBeNull();
            checkoutApi.HostedPaymentsClient().ShouldNotBeNull();
        }

        [Fact]
        public void ShouldInstantiateAndRetrieveClientsFour()
        {
            //Arrange
            var sdkCredentialsMock = new Mock<SdkCredentials>(MockBehavior.Strict, PlatformType.Default);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(mock => mock.CreateClient())
                .Returns(new HttpClient());
            var checkoutConfiguration = new CheckoutConfiguration(sdkCredentialsMock.Object, Environment.Sandbox,
                httpClientFactoryMock.Object);

            //Act
            var checkoutApi = new Four.CheckoutApi(checkoutConfiguration);
            
            //Assert
            checkoutApi.TokensClient().ShouldNotBeNull();
            checkoutApi.CustomersClient().ShouldNotBeNull();
            checkoutApi.PaymentsClient().ShouldNotBeNull();
            checkoutApi.InstrumentsClient().ShouldNotBeNull();
            checkoutApi.DisputesClient().ShouldNotBeNull();
            checkoutApi.RiskClient().ShouldNotBeNull();
            checkoutApi.ForexClient().ShouldNotBeNull();
            checkoutApi.WorkflowsClient().ShouldNotBeNull();
            checkoutApi.SessionsClient().ShouldNotBeNull();
            checkoutApi.MarketplaceClient().ShouldNotBeNull();
        }
    }
}
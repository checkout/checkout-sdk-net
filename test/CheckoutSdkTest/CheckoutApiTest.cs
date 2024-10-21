using Moq;
using Shouldly;
using System.Net.Http;
using Xunit;

namespace Checkout
{
    public class CheckoutApiTest : UnitTestFixture
    {
        [Fact]
        public void ShouldInstantiateAndRetrieveClientsPrevious()
        {
            //Arrange
            var sdkCredentialsMock = new Mock<SdkCredentials>(MockBehavior.Strict, PlatformType.Previous);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(mock => mock.CreateClient())
                .Returns(new HttpClient());
            var checkoutConfiguration = new CheckoutConfiguration(sdkCredentialsMock.Object, Environment.Sandbox, null,
                httpClientFactoryMock.Object, false);

            //Act
            Previous.ICheckoutApi checkoutApi = new Previous.CheckoutApi(checkoutConfiguration);

            //Assert
            checkoutApi.TokensClient().ShouldNotBeNull();
            checkoutApi.CustomersClient().ShouldNotBeNull();
            checkoutApi.SourcesClient().ShouldNotBeNull();
            checkoutApi.PaymentsClient().ShouldNotBeNull();
            checkoutApi.InstrumentsClient().ShouldNotBeNull();
            checkoutApi.DisputesClient().ShouldNotBeNull();
            checkoutApi.WebhooksClient().ShouldNotBeNull();
            checkoutApi.EventsClient().ShouldNotBeNull();
            checkoutApi.RiskClient().ShouldNotBeNull();
            checkoutApi.PaymentLinksClient().ShouldNotBeNull();
            checkoutApi.ReconciliationClient().ShouldNotBeNull();
            checkoutApi.HostedPaymentsClient().ShouldNotBeNull();
            checkoutApi.IdealClient().ShouldNotBeNull();
            checkoutApi.KlarnaClient().ShouldNotBeNull();
            checkoutApi.SepaClient().ShouldNotBeNull();
        }

        [Fact]
        public void ShouldInstantiateAndRetrieveClientsDefault()
        {
            //Arrange
            var sdkCredentialsMock = new Mock<SdkCredentials>(MockBehavior.Strict, PlatformType.Previous);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(mock => mock.CreateClient())
                .Returns(new HttpClient());
            var checkoutConfiguration = new CheckoutConfiguration(sdkCredentialsMock.Object, Environment.Sandbox, null,
                httpClientFactoryMock.Object, false);

            //Act
            ICheckoutApi checkoutApi = new CheckoutApi(checkoutConfiguration);

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
            checkoutApi.AccountsClient().ShouldNotBeNull();
            checkoutApi.PaymentLinksClient().ShouldNotBeNull();
            checkoutApi.HostedPaymentsClient().ShouldNotBeNull();
            checkoutApi.BalancesClient().ShouldNotBeNull();
            checkoutApi.TransfersClient().ShouldNotBeNull();
            checkoutApi.MetadataClient().ShouldNotBeNull();
        }
    }
}
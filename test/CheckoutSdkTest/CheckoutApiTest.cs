using Moq;
using Shouldly;
using System.Net.Http;
using Xunit;

namespace Checkout
{
    public class CheckoutApiTest : UnitTestFixture
    {
        [Fact]
        public void ShouldInstantiateAndRetrieveClientsPreviousClientFactoryAndHttpClient()
        {
            //Arrange
            var sdkCredentialsMock = new Mock<SdkCredentials>(MockBehavior.Strict, PlatformType.Previous);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(mock => mock.CreateClient())
                .Returns(new HttpClient());
            var httpClientMock = new Mock<HttpClient>();
            var checkoutConfiguration = new CheckoutConfiguration(sdkCredentialsMock.Object, Environment.Sandbox,
                httpClientFactoryMock.Object, httpClientMock.Object);

            CheckoutApiPreviousAssertions(checkoutConfiguration);
        }

        [Fact]
        public void ShouldInstantiateAndRetrieveClientsPreviousClientFactory()
        {
            //Arrange
            var sdkCredentialsMock = new Mock<SdkCredentials>(MockBehavior.Strict, PlatformType.Previous);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(mock => mock.CreateClient())
                .Returns(new HttpClient());
            var checkoutConfiguration = new CheckoutConfiguration(sdkCredentialsMock.Object, Environment.Sandbox,
                httpClientFactoryMock.Object, null);

            CheckoutApiPreviousAssertions(checkoutConfiguration);
        }

        [Fact]
        public void ShouldInstantiateAndRetrieveClientsPreviousHttpClient()
        {
            //Arrange
            var sdkCredentialsMock = new Mock<SdkCredentials>(MockBehavior.Strict, PlatformType.Previous);
            var httpClientMock = new Mock<HttpClient>();
            var checkoutConfiguration = new CheckoutConfiguration(sdkCredentialsMock.Object, Environment.Sandbox,
                null, httpClientMock.Object);

            CheckoutApiPreviousAssertions(checkoutConfiguration);
        }
        
        private static void CheckoutApiPreviousAssertions(CheckoutConfiguration checkoutConfiguration)
        {
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
            var httpClientMock = new Mock<HttpClient>();
            var checkoutConfiguration = new CheckoutConfiguration(sdkCredentialsMock.Object, Environment.Sandbox,
                httpClientFactoryMock.Object, httpClientMock.Object);

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
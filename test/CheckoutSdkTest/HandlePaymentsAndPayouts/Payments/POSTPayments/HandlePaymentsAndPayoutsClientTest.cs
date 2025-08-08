using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest;
using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Source;
using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.
    CardDestination;
using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.Common.
    AccountHolder.IndividualAccountHolder;
using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated;
using Checkout.Common;
using Checkout.Payments;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments
{
    public class HandlePaymentsAndPayoutsClientTest : UnitTestFixture
    {
        private const string PaymentsPath = "payments";

        private readonly SdkAuthorization _authorization =
            new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);

        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public HandlePaymentsAndPayoutsClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object, Environment.Sandbox,
                _httpClientFactory.Object);
        }

        [Fact]
        public async Task ShouldRequestPaymentWithUnreferencedRefund_WhenSuccessful()
        {
            // Arrange
            var request = CreateUnreferencedRefundRequest();
            var response = new RequestAPaymentOrPayoutResponseCreated();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<HttpMetadata>(
                        "payments",
                        _authorization,
                        It.IsAny<IDictionary<int, Type>>(),
                        request,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(response);

            var client = new PaymentsClient(_apiClient.Object, _configuration.Object);

            // Act
            var result = await client.RequestPayment(request);

            // Assert
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task ShouldThrowCheckoutArgumentException_WhenRequestIsNull()
        {
            // Arrange
            var client = new PaymentsClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.RequestPayment((UnreferencedRefundRequest)null));
        }

        private UnreferencedRefundRequest CreateUnreferencedRefundRequest()
        {
            return new UnreferencedRefundRequest
            {
                Amount = 1000,
                Currency = Currency.USD,
                PaymentType = "UnreferencedRefund",
                Reference = "test-reference",
                Source = new Source { Type = "currency_account", Id = "ca_test_12345678901234567890123456" },
                Destination = new CardDestination
                {
                    Number = "4242424242424242",
                    ExpiryMonth = 12,
                    ExpiryYear = 2025,
                    AccountHolder = new IndividualAccountHolder { FirstName = "John", LastName = "Doe" }
                },
                ProcessingChannelId = "pc_test_12345"
            };
        }
    }
}
using Checkout.Common;
using Checkout.Payments;
using Checkout.Payments.Setups;
using Checkout.Payments.Setups.Entities;
using Checkout.Payments.Setups.Requests;
using Checkout.Payments.Setups.Responses;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.PaymentSetups
{
    public class PaymentSetupsClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Default, ValidDefaultPk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Default);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public PaymentSetupsClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        public async Task CreatePaymentSetup_WhenRequestIsValid_ShouldSucceed()
        {
            // Arrange
            var request = CreateValidPaymentSetupsRequest();
            var expectedResponse = new PaymentSetupsResponse { Id = "ps_test_12345" };

            _apiClient.Setup(apiClient => apiClient.Post<PaymentSetupsResponse>(
                    "payments/setups", 
                    _authorization, 
                    request,
                    CancellationToken.None, 
                    null))
                .ReturnsAsync(expectedResponse);

            IPaymentSetupsClient paymentSetupsClient = new PaymentSetupsClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await paymentSetupsClient.CreatePaymentSetup(request);

            // Assert
            response.ShouldNotBeNull();
            response.Id.ShouldBe("ps_test_12345");
        }

        [Fact]
        public async Task UpdatePaymentSetup_WhenRequestIsValid_ShouldSucceed()
        {
            // Arrange
            var paymentSetupId = "ps_test_12345";
            var request = CreateValidPaymentSetupsRequest();
            var expectedResponse = new PaymentSetupsResponse { Id = paymentSetupId };

            _apiClient.Setup(apiClient => apiClient.Put<PaymentSetupsResponse>(
                    $"payments/setups/{paymentSetupId}", 
                    _authorization, 
                    request,
                    CancellationToken.None, 
                    null, 
                    null))
                .ReturnsAsync(expectedResponse);

            IPaymentSetupsClient paymentSetupsClient = new PaymentSetupsClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await paymentSetupsClient.UpdatePaymentSetup(paymentSetupId, request);

            // Assert
            response.ShouldNotBeNull();
            response.Id.ShouldBe(paymentSetupId);
        }

        [Fact]
        public async Task GetPaymentSetup_WhenIdIsValid_ShouldSucceed()
        {
            // Arrange
            var paymentSetupId = "ps_test_12345";
            var expectedResponse = new PaymentSetupsResponse { Id = paymentSetupId };

            _apiClient.Setup(apiClient => apiClient.Get<PaymentSetupsResponse>(
                    $"payments/setups/{paymentSetupId}", 
                    _authorization,
                    CancellationToken.None))
                .ReturnsAsync(expectedResponse);

            IPaymentSetupsClient paymentSetupsClient = new PaymentSetupsClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await paymentSetupsClient.GetPaymentSetup(paymentSetupId);

            // Assert
            response.ShouldNotBeNull();
            response.Id.ShouldBe(paymentSetupId);
        }

        [Fact]
        public async Task ConfirmPaymentSetup_WhenParametersAreValid_ShouldSucceed()
        {
            // Arrange
            var paymentSetupId = "ps_test_12345";
            var paymentMethodName = "card";
            var expectedResponse = new PaymentSetupsConfirmResponse { Id = "pay_test_confirm_111" };

            // IApiClient.Post has five parameters (path, authorization, request, cancellationToken,
            // idempotencyKey); the mock matches all five, with any request body.
            _apiClient.Setup(apiClient => apiClient.Post<PaymentSetupsConfirmResponse>(
                    $"payments/setups/{paymentSetupId}/confirm/{paymentMethodName}",
                    _authorization,
                    It.IsAny<object>(),
                    It.IsAny<CancellationToken>(),
                    It.IsAny<string>()))
                .ReturnsAsync(expectedResponse);

            IPaymentSetupsClient paymentSetupsClient = new PaymentSetupsClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await paymentSetupsClient.ConfirmPaymentSetup(paymentSetupId, paymentMethodName);

            // Assert
            response.ShouldNotBeNull();
            response.Id.ShouldBe("pay_test_confirm_111");
        }

        [Fact]
        public async Task ConfirmPaymentSetup_WithCashApp_ShouldCallCashAppConfirmPath()
        {
            // Arrange
            var paymentSetupId = "ps_test_12345";
            var expectedResponse = new PaymentSetupsConfirmResponse { Id = "pay_test_confirm_cashapp" };

            _apiClient.Setup(apiClient => apiClient.Post<PaymentSetupsConfirmResponse>(
                    $"payments/setups/{paymentSetupId}/confirm/cashapp",
                    _authorization,
                    It.IsAny<object>(),
                    It.IsAny<CancellationToken>(),
                    It.IsAny<string>()))
                .ReturnsAsync(expectedResponse);

            IPaymentSetupsClient paymentSetupsClient = new PaymentSetupsClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await paymentSetupsClient.ConfirmPaymentSetup(paymentSetupId, "cashapp");

            // Assert
            response.ShouldBeSameAs(expectedResponse);
        }

        [Fact]
        public async Task ConfirmPaymentSetup_ShouldSendNoBodyAndPassTheCancellationToken()
        {
            // Arrange
            var paymentSetupId = "ps_test_12345";
            var expectedResponse = new PaymentSetupsConfirmResponse { Id = "pay_test_confirm_token" };
            var cancellationToken = new CancellationTokenSource().Token;

            // Exact arguments: the confirm endpoint takes no body, and the caller's token must reach
            // the HTTP call rather than being bound to the request parameter.
            _apiClient.Setup(apiClient => apiClient.Post<PaymentSetupsConfirmResponse>(
                    $"payments/setups/{paymentSetupId}/confirm/cashapp",
                    _authorization,
                    null,
                    cancellationToken,
                    null))
                .ReturnsAsync(expectedResponse);

            IPaymentSetupsClient paymentSetupsClient = new PaymentSetupsClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await paymentSetupsClient.ConfirmPaymentSetup(paymentSetupId, "cashapp", cancellationToken);

            // Assert
            response.ShouldBeSameAs(expectedResponse);
        }

        private PaymentSetupsRequest CreateValidPaymentSetupsRequest()
        {
            return new PaymentSetupsRequest
            {
                ProcessingChannelId = "pc_test_12345",
                Amount = 1000,
                Currency = Currency.GBP,
                PaymentType = PaymentType.Regular,
                Reference = "TEST-REF-001",
                Description = "Test payment setup",
                Settings = new Settings
                {
                    SuccessUrl = "https://example.com/success",
                    FailureUrl = "https://example.com/failure"
                }
            };
        }
    }
}
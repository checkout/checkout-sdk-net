using Checkout.HandlePaymentsAndPayouts.Flow.Requests;
using Checkout.HandlePaymentsAndPayouts.Flow.Responses;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.Flow
{
    public class FlowClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;
        private readonly FlowClient _flowClient;

        public FlowClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
            
            _flowClient = new FlowClient(_apiClient.Object, _configuration.Object);
        }

        [Fact]
        public async Task CreatePaymentSession_Should_Call_ApiClient_Post()
        {
            // Arrange
            var request = new PaymentSessionCreateRequest();
            var response = new PaymentSessionResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Post<PaymentSessionResponse>(
                        "payment-sessions",
                        _authorization,
                        request,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(response);

            // Act
            var result = await _flowClient.RequestPaymentSession(request);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            _apiClient.Verify(apiClient =>
                apiClient.Post<PaymentSessionResponse>(
                    "payment-sessions",
                    _authorization,
                    request,
                    CancellationToken.None,
                    null), Times.Once);
        }

        [Fact]
        public async Task SubmitPaymentSession_Should_Call_ApiClient_Post()
        {
            // Arrange
            const string sessionId = "session_123";
            var request = new PaymentSessionSubmitRequest();
            var response = new ApprovedPaymentSubmissionResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Post<PaymentSubmissionResponse>(
                        "payment-sessions/session_123/submit",
                        _authorization,
                        request,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(response);

            // Act
            var result = await _flowClient.SubmitPaymentSession(sessionId, request);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            _apiClient.Verify(apiClient =>
                apiClient.Post<PaymentSubmissionResponse>(
                    "payment-sessions/session_123/submit",
                    _authorization,
                    request,
                    CancellationToken.None,
                    null), Times.Once);
        }

        [Fact]
        public async Task CompletePaymentSession_Should_Call_ApiClient_Post()
        {
            // Arrange
            var request = new PaymentSessionCompleteRequest();
            var response = new ApprovedPaymentSubmissionResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Post<PaymentSubmissionResponse>(
                        "payment-sessions/complete",
                        _authorization,
                        request,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(response);

            // Act
            var result = await _flowClient.RequestPaymentSessionWithPayment(request);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            _apiClient.Verify(apiClient =>
                apiClient.Post<PaymentSubmissionResponse>(
                    "payment-sessions/complete",
                    _authorization,
                    request,
                    CancellationToken.None,
                    null), Times.Once);
        }

        [Fact]
        public async Task CreatePaymentSession_Should_Validate_Request()
        {
            // Act & Assert
            await Should.ThrowAsync<CheckoutArgumentException>(async () => await _flowClient.RequestPaymentSession(null));
        }

        [Fact]
        public async Task SubmitPaymentSession_Should_Validate_Parameters()
        {
            // Act & Assert
            await Should.ThrowAsync<CheckoutArgumentException>(async () => await _flowClient.SubmitPaymentSession(null, new PaymentSessionSubmitRequest()));
            await Should.ThrowAsync<CheckoutArgumentException>(async () => await _flowClient.SubmitPaymentSession("session_123", null));
        }

        [Fact]
        public async Task CompletePaymentSession_Should_Validate_Request()
        {
            // Act & Assert
            await Should.ThrowAsync<CheckoutArgumentException>(async () => await _flowClient.RequestPaymentSessionWithPayment(null));
        }
    }
}
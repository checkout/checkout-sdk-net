using Checkout.HandlePaymentsAndPayouts.Payments.POSTPaymentsIdReversals.Requests.ReverseAPaymentRequest;
using Checkout.HandlePaymentsAndPayouts.Payments.POSTPaymentsIdReversals.Responses.ReverseAPaymentResponse;
using Checkout.Payments;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPaymentsIdReversals
{
    public class HandleReversalsClientTest : UnitTestFixture
    {
        private const string TestPaymentId = "pay_test_12345678901234567890123456";
        private const string TestActionId = "act_test_12345678901234567890123456";
        private const string TestReference = "test-reversal-reference";
        private const string TestIdempotencyKey = "test-idempotency-key";
        private const string ReversalsPath = "payments/{0}/reversals";

        private readonly SdkAuthorization _authorization =
            new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);

        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;
        private readonly PaymentsClient _client;

        public HandleReversalsClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object, Environment.Sandbox,
                _httpClientFactory.Object);
            
            _client = new PaymentsClient(_apiClient.Object, _configuration.Object);
        }

        [Fact]
        public async Task ShouldReversePayment_WhenSuccessful()
        {
            // Arrange
            var request = CreateReverseAPaymentRequest();
            var response = CreateReverseAPaymentResponse();

            SetupApiClientMock(request, response);

            // Act
            var result = await _client.ReverseAPayment(TestPaymentId, request);

            // Assert
            AssertSuccessfulResponse(result, TestActionId, TestReference);
        }

        [Fact]
        public async Task ShouldReversePaymentWithNullRequest_WhenSuccessful()
        {
            // Arrange
            var response = CreateReverseAPaymentResponse(includeReference: false);

            SetupApiClientMockForNullRequest(response);

            // Act
            var result = await _client.ReverseAPayment(TestPaymentId);

            // Assert
            AssertSuccessfulResponse(result, TestActionId);
        }

        [Fact]
        public async Task ShouldThrowCheckoutArgumentException_WhenPaymentIdIsNull()
        {
            // Arrange
            var request = CreateReverseAPaymentRequest();

            // Act & Assert
            await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await _client.ReverseAPayment(null, request));
        }

        [Fact]
        public async Task ShouldThrowCheckoutArgumentException_WhenPaymentIdIsEmpty()
        {
            // Arrange
            var request = CreateReverseAPaymentRequest();

            // Act & Assert
            await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await _client.ReverseAPayment(string.Empty, request));
        }

        [Fact]
        public async Task ShouldReversePayment_WithIdempotencyKey_WhenSuccessful()
        {
            // Arrange
            var request = CreateReverseAPaymentRequest();
            var response = CreateReverseAPaymentResponse();

            SetupApiClientMock(request, response, TestIdempotencyKey);

            // Act
            var result = await _client.ReverseAPayment(TestPaymentId, request, TestIdempotencyKey);

            // Assert
            AssertSuccessfulResponse(result, TestActionId, TestReference);
        }

        [Fact]
        public async Task ShouldReversePayment_WithIdempotencyKeyAndNullRequest_WhenSuccessful()
        {
            // Arrange
            var response = CreateReverseAPaymentResponse(includeReference: false);

            SetupApiClientMockForNullRequest(response, TestIdempotencyKey);

            // Act
            var result = await _client.ReverseAPayment(TestPaymentId, null, TestIdempotencyKey);

            // Assert
            AssertSuccessfulResponse(result, TestActionId);
        }

        [Fact]
        public async Task ShouldReversePayment_WithCustomCancellationToken_WhenSuccessful()
        {
            // Arrange
            var request = CreateReverseAPaymentRequest();
            var cancellationToken = new CancellationToken();
            var response = CreateReverseAPaymentResponse();

            SetupApiClientMock(request, response, cancellationToken: cancellationToken);

            // Act
            var result = await _client.ReverseAPayment(TestPaymentId, request, null, cancellationToken);

            // Assert
            AssertSuccessfulResponse(result, TestActionId, TestReference);
        }

        [Fact]
        public async Task ShouldReturnSameResult_WhenSameIdempotencyKeyUsedTwice()
        {
            // Arrange
            var request = CreateReverseAPaymentRequest();
            var idempotencyKey = "test-idempotency-key-123";
            var expectedResponse = CreateReverseAPaymentResponse();

            SetupApiClientMock(request, expectedResponse, idempotencyKey);

            // Act - First call
            var firstResult = await _client.ReverseAPayment(TestPaymentId, request, idempotencyKey);
            
            // Act - Second call with same idempotency key
            var secondResult = await _client.ReverseAPayment(TestPaymentId, request, idempotencyKey);

            // Assert - Both calls should return the same result
            AssertIdempotentResults(firstResult, secondResult);

            // Verify the API was called twice with the same parameters
            VerifyApiClientCalledTwice(request, idempotencyKey);
        }

        private ReverseAPaymentRequest CreateReverseAPaymentRequest()
        {
            return new ReverseAPaymentRequest
            {
                Reference = TestReference,
                Metadata = new { OrderId = "order_123", CustomField = "test_value" }
            };
        }

        private ReverseAPaymentResponse CreateReverseAPaymentResponse(bool includeReference = true)
        {
            var response = new ReverseAPaymentResponse
            {
                ActionId = TestActionId
            };

            if (includeReference)
            {
                response.Reference = TestReference;
            }

            return response;
        }

        private void SetupApiClientMock(
            ReverseAPaymentRequest request, 
            ReverseAPaymentResponse response, 
            string idempotencyKey = null, 
            CancellationToken cancellationToken = default)
        {
            _apiClient.Setup(apiClient =>
                    apiClient.Post<ReverseAPaymentResponse>(
                        string.Format(ReversalsPath, TestPaymentId),
                        _authorization,
                        request,
                        cancellationToken == default ? CancellationToken.None : cancellationToken,
                        idempotencyKey))
                .ReturnsAsync(response);
        }

        private void SetupApiClientMockForNullRequest(
            ReverseAPaymentResponse response, 
            string idempotencyKey = null)
        {
            _apiClient.Setup(apiClient =>
                    apiClient.Post<ReverseAPaymentResponse>(
                        string.Format(ReversalsPath, TestPaymentId),
                        _authorization,
                        It.IsAny<ReverseAPaymentRequest>(),
                        CancellationToken.None,
                        idempotencyKey))
                .ReturnsAsync(response);
        }

        private static void AssertSuccessfulResponse(ReverseAPaymentResponse result, string expectedActionId, string expectedReference = null)
        {
            result.ShouldNotBeNull();
            result.ActionId.ShouldBe(expectedActionId);
            
            if (expectedReference != null)
            {
                result.Reference.ShouldBe(expectedReference);
            }
        }

        private static void AssertIdempotentResults(ReverseAPaymentResponse firstResult, ReverseAPaymentResponse secondResult)
        {
            firstResult.ShouldNotBeNull();
            secondResult.ShouldNotBeNull();
            firstResult.ActionId.ShouldBe(secondResult.ActionId);
            firstResult.Reference.ShouldBe(secondResult.Reference);
            firstResult.ActionId.ShouldBe(TestActionId);
            firstResult.Reference.ShouldBe(TestReference);
        }

        private void VerifyApiClientCalledTwice(ReverseAPaymentRequest request, string idempotencyKey)
        {
            _apiClient.Verify(apiClient =>
                apiClient.Post<ReverseAPaymentResponse>(
                    string.Format(ReversalsPath, TestPaymentId),
                    _authorization,
                    request,
                    CancellationToken.None,
                    idempotencyKey), Times.Exactly(2));
        }
    }
}
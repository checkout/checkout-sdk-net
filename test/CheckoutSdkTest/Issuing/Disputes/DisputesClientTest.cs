using Checkout.Common;
using Checkout.Issuing.Disputes.Common;
using Checkout.Issuing.Disputes.Requests;
using Checkout.Issuing.Disputes.Responses;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Issuing.Disputes
{
    public class DisputesClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public DisputesClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        public async Task CreateDispute_WhenRequestIsValid_ShouldSucceed()
        {
            // Arrange
            var idempotencyKey = Guid.NewGuid().ToString();
            var request = CreateValidCreateDisputeRequest();
            var expectedResponse = CreateValidIssuingDisputeResponse();

            _apiClient.Setup(apiClient => apiClient.Post<IssuingDisputeResponse>(
                    "issuing/disputes",
                    _authorization,
                    request,
                    CancellationToken.None,
                    idempotencyKey))
                .ReturnsAsync(expectedResponse);

            IIssuingClient issuingClient = new IssuingClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await issuingClient.CreateDispute(request, idempotencyKey);

            // Assert
            ValidateIssuingDisputeResponse(response, expectedResponse);
        }

        [Fact]
        public async Task GetDisputeDetails_WhenDisputeIdIsValid_ShouldSucceed()
        {
            // Arrange
            var disputeId = "idsp_test_12345abcdefghijklmnop";
            var expectedResponse = CreateValidIssuingDisputeResponse();
            expectedResponse.Id = disputeId;

            _apiClient.Setup(apiClient => apiClient.Get<IssuingDisputeResponse>(
                    $"issuing/disputes/{disputeId}",
                    _authorization,
                    CancellationToken.None))
                .ReturnsAsync(expectedResponse);

            IIssuingClient issuingClient = new IssuingClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await issuingClient.GetDisputeDetails(disputeId);

            // Assert
            ValidateIssuingDisputeResponse(response, expectedResponse);
        }

        [Fact]
        public async Task CancelDispute_WhenDisputeIdIsValid_ShouldSucceed()
        {
            // Arrange
            var idempotencyKey = Guid.NewGuid().ToString();
            var disputeId = "idsp_test_12345abcdefghijklmnop";
            var expectedResponse = new EmptyResponse();

            _apiClient.Setup(apiClient => apiClient.Post<EmptyResponse>(
                    $"issuing/disputes/{disputeId}/cancel",
                    _authorization,
                    null,
                    CancellationToken.None,
                    idempotencyKey))
                .ReturnsAsync(expectedResponse);

            IIssuingClient issuingClient = new IssuingClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await issuingClient.CancelDispute(disputeId, idempotencyKey);

            // Assert
            response.ShouldNotBeNull();
        }

        [Fact]
        public async Task EscalateDispute_WhenRequestIsValid_ShouldSucceed()
        {
            // Arrange
            var idempotencyKey = Guid.NewGuid().ToString();
            var disputeId = "idsp_test_12345abcdefghijklmnop";
            var request = CreateValidEscalateDisputeRequest();
            var expectedResponse = new EmptyResponse();

            _apiClient.Setup(apiClient => apiClient.Post<EmptyResponse>(
                    $"issuing/disputes/{disputeId}/escalate",
                    _authorization,
                    request,
                    CancellationToken.None,
                    idempotencyKey))
                .ReturnsAsync(expectedResponse);

            IIssuingClient issuingClient = new IssuingClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await issuingClient.EscalateDispute(disputeId, idempotencyKey, request);

            // Assert
            response.ShouldNotBeNull();
        }

        [Fact]
        public async Task SubmitDispute_WhenRequestIsValid_ShouldSucceed()
        {
            // Arrange
            var idempotencyKey = Guid.NewGuid().ToString();
            var disputeId = "idsp_test_12345abcdefghijklmnop";
            var request = CreateValidSubmitDisputeRequest();
            var expectedResponse = CreateValidIssuingDisputeResponse();
            expectedResponse.Id = disputeId;

            _apiClient.Setup(apiClient => apiClient.Post<IssuingDisputeResponse>(
                    $"issuing/disputes/{disputeId}/submit",
                    _authorization,
                    request,
                    CancellationToken.None,
                    idempotencyKey))
                .ReturnsAsync(expectedResponse);

            IIssuingClient issuingClient = new IssuingClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await issuingClient.SubmitDispute(disputeId, idempotencyKey, request);

            // Assert
            ValidateIssuingDisputeResponse(response, expectedResponse);
        }

        [Fact]
        public async Task SubmitDispute_WhenRequestIsNull_ShouldSucceed()
        {
            // Arrange
            var idempotencyKey = Guid.NewGuid().ToString();
            var disputeId = "idsp_test_12345abcdefghijklmnop";
            var expectedResponse = CreateValidIssuingDisputeResponse();
            expectedResponse.Id = disputeId;

            _apiClient.Setup(apiClient => apiClient.Post<IssuingDisputeResponse>(
                    $"issuing/disputes/{disputeId}/submit",
                    _authorization,
                    null,
                    CancellationToken.None,
                    idempotencyKey))
                .ReturnsAsync(expectedResponse);

            IIssuingClient issuingClient = new IssuingClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await issuingClient.SubmitDispute(disputeId, idempotencyKey);

            // Assert
            ValidateIssuingDisputeResponse(response, expectedResponse);
        }

        // Setup Methods (Builders)
        private CreateDisputeRequest CreateValidCreateDisputeRequest()
        {
            return new CreateDisputeRequest
            {
                TransactionId = "trx_test_abcdefghijklmnopqr",
                Reason = "4837",
                Evidence = new List<DisputeEvidence>
                {
                    new DisputeEvidence
                    {
                        Name = "receipt.pdf",
                        Content = "SGVsbG8gV29ybGQ=",
                        Description = "Transaction receipt"
                    }
                },
                Amount = 1000,
                PresentmentMessageId = "msg_test_abcdefghijklmnopqr",
                IsReadyForSubmission = false,
                Justification = "Customer dispute"
            };
        }

        private EscalateDisputeRequest CreateValidEscalateDisputeRequest()
        {
            return new EscalateDisputeRequest
            {
                Justification = "Escalating due to additional evidence",
                AdditionalEvidence = new List<DisputeEvidence>
                {
                    new DisputeEvidence
                    {
                        Name = "additional_evidence.pdf",
                        Content = "QWRkaXRpb25hbCBFdmlkZW5jZQ==",
                        Description = "Additional supporting documentation"
                    }
                },
                Amount = 500
            };
        }

        private SubmitDisputeRequest CreateValidSubmitDisputeRequest()
        {
            return new SubmitDisputeRequest
            {
                Reason = "4855",
                Evidence = new List<DisputeEvidence>
                {
                    new DisputeEvidence
                    {
                        Name = "updated_evidence.pdf",
                        Content = "VXBkYXRlZCBFdmlkZW5jZQ==",
                        Description = "Updated evidence file"
                    }
                },
                Amount = 750
            };
        }

        private IssuingDisputeResponse CreateValidIssuingDisputeResponse()
        {
            return new IssuingDisputeResponse
            {
                Id = "idsp_test_12345abcdefghijklmnop",
                Reason = "4837",
                DisputedAmount = new DisputeAmount
                {
                    Amount = 1000,
                    Currency = Currency.USD
                },
                Status = IssuingDisputeStatus.Created,
                StatusReason = IssuingDisputeStatusReason.ChargebackPending,
                TransactionId = "trx_test_abcdefghijklmnopqr"
            };
        }

        // Validation Methods (Asserts)
        private void ValidateIssuingDisputeResponse(IssuingDisputeResponse actual, IssuingDisputeResponse expected)
        {
            actual.ShouldNotBeNull();
            actual.Id.ShouldBe(expected.Id);
            actual.Reason.ShouldBe(expected.Reason);
            actual.Status.ShouldBe(expected.Status);
            actual.StatusReason.ShouldBe(expected.StatusReason);
            actual.TransactionId.ShouldBe(expected.TransactionId);
            
            if (expected.DisputedAmount != null)
            {
                actual.DisputedAmount.ShouldNotBeNull();
                actual.DisputedAmount.Amount.ShouldBe(expected.DisputedAmount.Amount);
                actual.DisputedAmount.Currency.ShouldBe(expected.DisputedAmount.Currency);
            }
        }
    }
}
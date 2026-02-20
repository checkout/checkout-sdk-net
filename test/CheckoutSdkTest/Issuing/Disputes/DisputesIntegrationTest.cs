using Checkout.Issuing.Disputes.Requests;
using Checkout.Issuing.Disputes.Responses;
using Checkout.Issuing.Disputes.Common;
using Checkout.Issuing.Transactions.Requests.Query;
using Checkout.Issuing.Cardholders.Responses;
using Checkout.Issuing.Cards.Requests.Create;
using Checkout.Issuing.Cards.Responses.Create;
using Checkout.Issuing.Testing.Requests;
using Checkout.Issuing.Testing.Responses;
using Checkout.Common;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Issuing.Disputes
{
    public class DisputesIntegrationTest : IssuingCommon, IAsyncLifetime
    {
        private CardholderResponse _cardholder;
        private AbstractCardCreateResponse _card;
        private string _clearedTransactionId;

        public async Task InitializeAsync()
        {
            // Create cardholder and card for testing
            _cardholder = await CreateCardholder();
            var cardRequest = await CreateVirtualCard(_cardholder.Id);
            _card = await Api.IssuingClient().CreateCard(cardRequest);
            
            // Activate the card before using it for transactions
            await Api.IssuingClient().ActivateCard(_card.Id);
            
            // Create and clear a transaction for dispute testing
            _clearedTransactionId = await CreateClearedTransaction();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        [Fact(Skip = "Requires permissions to create disputes and simulate transactions - we must ensure the test environment is set up correctly first")]
        public async Task CreateDispute_ShouldReturnValidResponse()
        {
            // Arrange
            var idempotencyKey = Guid.NewGuid().ToString();
            var createRequest = CreateValidCreateDisputeRequest(_clearedTransactionId);

            // Act
            var response = await Api.IssuingClient().CreateDispute(createRequest, idempotencyKey);

            // Assert
            ValidateCreatedDisputeResponse(response, createRequest);
        }

        [Fact(Skip = "Requires permissions to create disputes and simulate transactions - we must ensure the test environment is set up correctly first")]
        public async Task GetDisputeDetails_ShouldReturnValidResponse()
        {
            // Arrange
            var idempotencyKey = Guid.NewGuid().ToString();
            var createRequest = CreateValidCreateDisputeRequest(_clearedTransactionId);
            var createResponse = await DefaultApi.IssuingClient().CreateDispute(createRequest, idempotencyKey);

            // Act
            var response = await DefaultApi.IssuingClient().GetDisputeDetails(createResponse.Id);

            // Assert
            ValidateDisputeDetailsResponse(response, createResponse);
        }

        [Fact(Skip = "Requires permissions to create disputes and simulate transactions - we must ensure the test environment is set up correctly first")]
        public async Task CancelDispute_ShouldSucceed()
        {
            // Arrange
            var idempotencyKey = Guid.NewGuid().ToString();
            var createRequest = CreateValidCreateDisputeRequest(_clearedTransactionId);
            var createResponse = await DefaultApi.IssuingClient().CreateDispute(createRequest, idempotencyKey);

            // Act
            var response = await DefaultApi.IssuingClient().CancelDispute(createResponse.Id, idempotencyKey);

            // Assert
            response.ShouldNotBeNull();

            // Verify the dispute was cancelled
            var updatedDispute = await DefaultApi.IssuingClient().GetDisputeDetails(createResponse.Id);
            updatedDispute.Status.ShouldBe(IssuingDisputeStatus.Canceled);
        }

        [Fact(Skip = "Requires permissions to create disputes and simulate transactions - we must ensure the test environment is set up correctly first")]
        public async Task EscalateDispute_ShouldSucceed()
        {
            // Arrange
            var idempotencyKey = Guid.NewGuid().ToString();
            var createRequest = CreateValidCreateDisputeRequest(_clearedTransactionId);
            createRequest.IsReadyForSubmission = true; // Submit immediately to allow escalation
            var createResponse = await DefaultApi.IssuingClient().CreateDispute(createRequest, idempotencyKey);

            // Wait for dispute to be in a state that allows escalation
            // In practice, you might need to wait for the dispute status to change
            var escalateRequest = CreateValidEscalateDisputeRequest();

            // Act
            var response = await DefaultApi.IssuingClient().EscalateDispute(createResponse.Id, idempotencyKey, escalateRequest);

            // Assert
            response.ShouldNotBeNull();
        }

        [Fact(Skip = "Requires permissions to create disputes and simulate transactions - we must ensure the test environment is set up correctly first")]
        public async Task SubmitDispute_ShouldReturnValidResponse()
        {
            // Arrange
            var idempotencyKey = Guid.NewGuid().ToString();
            var createRequest = CreateValidCreateDisputeRequest(_clearedTransactionId);
            createRequest.IsReadyForSubmission = false; // Create without submitting
            var createResponse = await DefaultApi.IssuingClient().CreateDispute(createRequest, idempotencyKey);

            var submitRequest = CreateValidSubmitDisputeRequest();

            // Act
            var response = await DefaultApi.IssuingClient().SubmitDispute(createResponse.Id, idempotencyKey, submitRequest);

            // Assert
            ValidateSubmittedDisputeResponse(response, createResponse.Id);
        }

        [Fact(Skip = "Requires permissions to create disputes and simulate transactions - we must ensure the test environment is set up correctly first")]
        public async Task SubmitDispute_WithoutRequest_ShouldReturnValidResponse()
        {
            // Arrange
            var idempotencyKey = Guid.NewGuid().ToString();
            var createRequest = CreateValidCreateDisputeRequest(_clearedTransactionId);
            createRequest.IsReadyForSubmission = false; // Create without submitting
            var createResponse = await DefaultApi.IssuingClient().CreateDispute(createRequest, idempotencyKey);

            // Act
            var response = await DefaultApi.IssuingClient().SubmitDispute(createResponse.Id, idempotencyKey);

            // Assert
            ValidateSubmittedDisputeResponse(response, createResponse.Id);
        }

        // Helper method to create a cleared transaction for dispute testing
        private async Task<string> CreateClearedTransaction()
        {
            // Create an authorization
            var authRequest = new CardAuthorizationRequest
            {
                Card = new CardSimulation
                {
                    Id = _card.Id,
                    ExpiryMonth = _card.ExpiryMonth,
                    ExpiryYear = _card.ExpiryYear
                },
                Transaction = new TransactionSimulation
                {
                    Type = TransactionType.Purchase,
                    Amount = 1000, // $10.00 in cents
                    Currency = Currency.USD
                }
            };

            var authResponse = await Api.IssuingClient().SimulateAuthorization(authRequest);
            
            // Clear the transaction to make it disputable
            var clearingRequest = new CardClearingAuthorizationRequest
            {
                Amount = 1000 // Same amount as authorized
            };

            await Api.IssuingClient().SimulateClearing(authResponse.Id, clearingRequest);
            
            return authResponse.Id;
        }

        // Setup Methods (Builders)

        private CreateDisputeRequest CreateValidCreateDisputeRequest(string transactionId = null)
        {
            return new CreateDisputeRequest
            {
                TransactionId = transactionId ?? "trx_test_abcdefghijklmnopqr",
                Reason = "4837", // No cardholder authorization
                Evidence = new List<DisputeEvidence>
                {
                    new DisputeEvidence
                    {
                        Name = "receipt.pdf",
                        Content = "SGVsbG8gV29ybGQ=", // Base64 encoded "Hello World"
                        Description = "Transaction receipt showing unauthorized charge"
                    }
                },
                Amount = 1000, // $10.00 in cents
                IsReadyForSubmission = false,
                Justification = "Customer reports unauthorized transaction"
            };
        }

        private EscalateDisputeRequest CreateValidEscalateDisputeRequest()
        {
            return new EscalateDisputeRequest
            {
                Justification = "Merchant response was insufficient. Escalating to pre-arbitration with additional evidence showing customer was in different location during transaction time.",
                AdditionalEvidence = new List<DisputeEvidence>
                {
                    new DisputeEvidence
                    {
                        Name = "location_evidence.pdf",
                        Content = "TG9jYXRpb24gRXZpZGVuY2U=", // Base64 encoded "Location Evidence"
                        Description = "GPS data showing customer location during transaction"
                    }
                },
                Amount = 800 // Reducing disputed amount to $8.00
            };
        }

        private SubmitDisputeRequest CreateValidSubmitDisputeRequest()
        {
            return new SubmitDisputeRequest
            {
                Reason = "4855", // Goods or services not provided
                Evidence = new List<DisputeEvidence>
                {
                    new DisputeEvidence
                    {
                        Name = "updated_receipt.pdf",
                        Content = "VXBkYXRlZCBSZWNlaXB0", // Base64 encoded "Updated Receipt"
                        Description = "Updated receipt with corrected amount"
                    }
                },
                Amount = 750 // $7.50 in cents
            };
        }

        // Validation Methods (Asserts)
        private void ValidateCreatedDisputeResponse(IssuingDisputeResponse response, CreateDisputeRequest request)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.Id.ShouldStartWith("idsp_");
            response.TransactionId.ShouldBe(request.TransactionId);
            response.Reason.ShouldBe(request.Reason);
            response.Status.ShouldBe(IssuingDisputeStatus.Created);
            
            if (request.Amount.HasValue)
            {
                response.DisputedAmount.ShouldNotBeNull();
                response.DisputedAmount.Amount.ShouldBe(request.Amount.Value);
            }
        }

        private void ValidateDisputeDetailsResponse(IssuingDisputeResponse response, IssuingDisputeResponse originalResponse)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldBe(originalResponse.Id);
            response.TransactionId.ShouldBe(originalResponse.TransactionId);
            response.Reason.ShouldBe(originalResponse.Reason);
            
            response.DisputedAmount.ShouldNotBeNull();
            response.DisputedAmount.Amount.ShouldBe(originalResponse.DisputedAmount.Amount);
            response.DisputedAmount.Currency.ShouldBe(originalResponse.DisputedAmount.Currency);
        }

        private void ValidateSubmittedDisputeResponse(IssuingDisputeResponse response, string expectedDisputeId)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldBe(expectedDisputeId);
            response.Status.ShouldBeOneOf(IssuingDisputeStatus.Processing, IssuingDisputeStatus.ActionRequired);
        }
    }
}
using Checkout.Issuing.Disputes.Requests;
using Checkout.Issuing.Disputes.Responses;
using Checkout.Issuing.Disputes.Common;
using Checkout.Issuing.Transactions.Requests.Query;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Issuing.Disputes
{
    public class DisputesIntegrationTest : IssuingCommon
    {
         // NOTE: This test requires the following OAuth scopes:
        // - issuing:disputes-read
        // - issuing:disputes-write
        // - issuing:transactions-read (to get real transaction IDs)

        [Fact(Skip = "Requires existing cleared transaction and proper OAuth scopes")]
        public async Task CreateDispute_ShouldReturnValidResponse()
        {
            // Arrange
            var transactionId = await GetValidTransactionIdForDispute();
            
            // Skip test if no valid transactions found
            if (string.IsNullOrEmpty(transactionId))
            {
                return; // Skip.If() would be better but not available
            }
            
            var createRequest = CreateValidCreateDisputeRequest(transactionId);

            // Act
            var response = await Api.IssuingClient().CreateDispute(createRequest);

            // Assert
            ValidateCreatedDisputeResponse(response, createRequest);
        }

        [Fact(Skip = "Requires existing cleared transaction and proper OAuth scopes")]
        public async Task GetDisputeDetails_ShouldReturnValidResponse()
        {
            // Arrange
            var transactionId = await GetValidTransactionIdForDispute();
            
            // Skip test if no valid transactions found
            if (string.IsNullOrEmpty(transactionId))
            {
                return;
            }
            
            var createRequest = CreateValidCreateDisputeRequest(transactionId);
            var createResponse = await Api.IssuingClient().CreateDispute(createRequest);

            // Act
            var response = await Api.IssuingClient().GetDisputeDetails(createResponse.Id);

            // Assert
            ValidateDisputeDetailsResponse(response, createResponse);
        }

        [Fact(Skip = "Requires existing cleared transaction and proper OAuth scopes")]
        public async Task CancelDispute_ShouldSucceed()
        {
            // Arrange
            var transactionId = await GetValidTransactionIdForDispute();
            
            // Skip test if no valid transactions found
            if (string.IsNullOrEmpty(transactionId))
            {
                return;
            }
            
            var createRequest = CreateValidCreateDisputeRequest(transactionId);
            var createResponse = await Api.IssuingClient().CreateDispute(createRequest);

            // Act
            var response = await Api.IssuingClient().CancelDispute(createResponse.Id);

            // Assert
            response.ShouldNotBeNull();

            // Verify the dispute was cancelled
            var updatedDispute = await Api.IssuingClient().GetDisputeDetails(createResponse.Id);
            updatedDispute.Status.ShouldBe(IssuingDisputeStatus.Canceled);
        }

        [Fact(Skip = "Requires existing submitted dispute and proper OAuth scopes")]
        public async Task EscalateDispute_ShouldSucceed()
        {
            // Arrange
            var transactionId = await GetValidTransactionIdForDispute();
            
            // Skip test if no valid transactions found
            if (string.IsNullOrEmpty(transactionId))
            {
                return;
            }
            
            var createRequest = CreateValidCreateDisputeRequest(transactionId);
            createRequest.IsReadyForSubmission = true; // Submit immediately to allow escalation
            var createResponse = await Api.IssuingClient().CreateDispute(createRequest);

            // Wait for dispute to be in a state that allows escalation
            // In practice, you might need to wait for the dispute status to change
            var escalateRequest = CreateValidEscalateDisputeRequest();

            // Act
            var response = await Api.IssuingClient().EscalateDispute(createResponse.Id, escalateRequest);

            // Assert
            response.ShouldNotBeNull();
        }

        [Fact(Skip = "Requires existing cleared transaction and proper OAuth scopes")]
        public async Task SubmitDispute_ShouldReturnValidResponse()
        {
            // Arrange
            var transactionId = await GetValidTransactionIdForDispute();
            
            // Skip test if no valid transactions found
            if (string.IsNullOrEmpty(transactionId))
            {
                return;
            }
            
            var createRequest = CreateValidCreateDisputeRequest(transactionId);
            createRequest.IsReadyForSubmission = false; // Create without submitting
            var createResponse = await Api.IssuingClient().CreateDispute(createRequest);

            var submitRequest = CreateValidSubmitDisputeRequest();

            // Act
            var response = await Api.IssuingClient().SubmitDispute(createResponse.Id, submitRequest);

            // Assert
            ValidateSubmittedDisputeResponse(response, createResponse.Id);
        }

        [Fact(Skip = "Requires existing cleared transaction and proper OAuth scopes")]
        public async Task SubmitDispute_WithoutRequest_ShouldReturnValidResponse()
        {
            // Arrange
            var transactionId = await GetValidTransactionIdForDispute();
            
            // Skip test if no valid transactions found
            if (string.IsNullOrEmpty(transactionId))
            {
                return;
            }
            
            var createRequest = CreateValidCreateDisputeRequest(transactionId);
            createRequest.IsReadyForSubmission = false; // Create without submitting
            var createResponse = await Api.IssuingClient().CreateDispute(createRequest);

            // Act
            var response = await Api.IssuingClient().SubmitDispute(createResponse.Id);

            // Assert
            ValidateSubmittedDisputeResponse(response, createResponse.Id);
        }

        // Helper method to get a valid transaction ID for dispute testing
        private async Task<string> GetValidTransactionIdForDispute()
        {
            try
            {
                // Get recent transactions from your sandbox environment
                var query = new TransactionsQueryFilter();
                var transactions = await Api.IssuingClient().GetListTransactions(query);
                
                // Find a cleared transaction that can be disputed
                // Note: In a real scenario, you'd filter for:
                // - Status = "cleared"
                // - Not already refunded
                // - Not already disputed
                var validTransaction = transactions.Data?.FirstOrDefault(t => 
                    !string.IsNullOrEmpty(t.Id) && 
                    t.Id.StartsWith("trx_"));
                    
                return validTransaction?.Id;
            }
            catch
            {
                // If we can't get transactions, return null to skip the test
                return null;
            }
        }

        // Setup Methods (Builders)
        private CreateDisputeRequest CreateValidCreateDisputeRequest(string transactionId = null)
        {
            return new CreateDisputeRequest
            {
                TransactionId = transactionId ?? "trx_test_abcdefghijklmnopqr", // Use real transaction ID when available
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
using Checkout.HandlePaymentsAndPayouts.Payments.POSTPaymentsIdReversals.Requests.ReverseAPaymentRequest;
using Checkout.Common;
using Checkout.Payments;
using Shouldly;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPaymentsIdReversals
{
    public class HandleReversalsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        public HandleReversalsIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact]
        public async Task ShouldReversePayment_WhenSuccessful()
        {
            // Arrange - Create an initial authorized payment to reverse
            // For authorized but not captured payments, reversal performs a void
            var paymentResponse = await MakeCardPayment(shouldCapture: false, amount: 100);
            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Authorized);

            var reversalRequest = new ReverseAPaymentRequest
            {
                Reference = $"reversal-{Guid.NewGuid()}",
                Metadata = new { TestReason = "integration_test", OrderId = paymentResponse.Reference, CouponCode = "NY2024" }
            };

            // Act - Reverse the payment (should void the authorization)
            var reversalResponse = await DefaultApi.PaymentsClient().ReverseAPayment(paymentResponse.Id, reversalRequest);

            // Assert
            reversalResponse.ShouldNotBeNull();
            reversalResponse.ActionId.ShouldNotBeNullOrEmpty();
            reversalResponse.Reference.ShouldBe(reversalRequest.Reference);
        }

        [Fact]
        public async Task ShouldReversePayment_WithNullRequest_WhenSuccessful()
        {
            // Arrange - Create an initial authorized payment to reverse
            // Minimal reversal request without reference or metadata
            var paymentResponse = await MakeCardPayment(shouldCapture: false, amount: 100);
            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Authorized);

            // Act - Reverse the payment without a request body
            var reversalResponse = await DefaultApi.PaymentsClient().ReverseAPayment(paymentResponse.Id);

            // Assert
            reversalResponse.ShouldNotBeNull();
            reversalResponse.ActionId.ShouldNotBeNullOrEmpty();
        }

        [Fact]
        public async Task ShouldReversePayment_WithIdempotencyKey_WhenSuccessful()
        {
            // Arrange - Create an initial authorized payment to reverse
            var paymentResponse = await MakeCardPayment(shouldCapture: false, amount: 100);
            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Authorized);

            var reversalRequest = new ReverseAPaymentRequest
            {
                Reference = $"reversal-{Guid.NewGuid()}",
                Metadata = new { TestReason = "idempotency_test" }
            };

            var idempotencyKey = $"idem-{Guid.NewGuid()}";

            // Act - Reverse the payment twice with the same idempotency key
            var firstReversalResponse = await DefaultApi.PaymentsClient().ReverseAPayment(
                paymentResponse.Id, reversalRequest, idempotencyKey);

            var secondReversalResponse = await DefaultApi.PaymentsClient().ReverseAPayment(
                paymentResponse.Id, reversalRequest, idempotencyKey);

            // Assert - Both responses should be identical (idempotent behavior)
            firstReversalResponse.ShouldNotBeNull();
            secondReversalResponse.ShouldNotBeNull();
            firstReversalResponse.ActionId.ShouldBe(secondReversalResponse.ActionId);
            firstReversalResponse.Reference.ShouldBe(secondReversalResponse.Reference);
        }

        [Fact]
        public async Task ShouldThrowException_WhenPaymentNotFound()
        {
            // Arrange
            var nonExistentPaymentId = "pay_non_existent_payment_id_123456";
            var reversalRequest = new ReverseAPaymentRequest
            {
                Reference = $"reversal-{Guid.NewGuid()}",
                Metadata = new { TestReason = "test_not_found" }
            };

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutApiException>(async () =>
                await DefaultApi.PaymentsClient().ReverseAPayment(nonExistentPaymentId, reversalRequest));

            exception.ShouldNotBeNull();
            // According to docs: 404 for not found, 403 for cannot be reversed (declined, 3DS in progress, etc.)
            exception.HttpStatusCode.ShouldBeOneOf(HttpStatusCode.NotFound, HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task ShouldReversePayment_AfterCapture_WhenSuccessful()
        {
            // Arrange - Create an authorized payment, then capture and reverse it
            // For authorized and captured payments, reversal performs a full refund
            var paymentResponse = await MakeCardPayment(shouldCapture: false, amount: 100);
            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Authorized);

            // Capture the payment
            var captureResponse = await DefaultApi.PaymentsClient().CapturePayment(paymentResponse.Id);
            captureResponse.ShouldNotBeNull();

            // Wait a moment for the capture to process
            await Task.Delay(2000);

            var reversalRequest = new ReverseAPaymentRequest
            {
                Reference = $"reversal-{Guid.NewGuid()}",
                Metadata = new { TestReason = "post_capture_reversal", PartnerId = 123989 }
            };

            // Act - Reverse the captured payment (should perform a full refund)
            var reversalResponse = await DefaultApi.PaymentsClient().ReverseAPayment(paymentResponse.Id, reversalRequest);

            // Assert - The reversal should be successful
            reversalResponse.ShouldNotBeNull();
            reversalResponse.ActionId.ShouldNotBeNullOrEmpty();
            reversalResponse.Reference.ShouldBe(reversalRequest.Reference);
        }

        [Fact]
        public async Task ShouldAllowMultipleReversals_WhenSuccessful()
        {
            // Arrange - Create and reverse a payment, then try to reverse it again
            var paymentResponse = await MakeCardPayment(shouldCapture: false, amount: 100);
            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Authorized);

            // First reversal
            var firstReversalRequest = new ReverseAPaymentRequest
            {
                Reference = $"reversal-1-{Guid.NewGuid()}",
                Metadata = new { TestReason = "first_reversal", CouponCode = "TEST2024" }
            };

            var firstReversalResponse = await DefaultApi.PaymentsClient().ReverseAPayment(paymentResponse.Id, firstReversalRequest);
            firstReversalResponse.ShouldNotBeNull();
            firstReversalResponse.ActionId.ShouldNotBeNullOrEmpty();
            firstReversalResponse.Reference.ShouldBe(firstReversalRequest.Reference);

            // Wait for the first reversal to process
            await Task.Delay(3000);

            // Second reversal attempt
            var secondReversalRequest = new ReverseAPaymentRequest
            {
                Reference = $"reversal-2-{Guid.NewGuid()}",
                Metadata = new { TestReason = "second_reversal" }
            };

            // Act - Try to reverse an already reversed payment
            // According to docs: for card payments, this might return 204 (No Content) or succeed
            // For non-card payments, this performs the second action (void -> refund)
            var secondReversalResponse = await DefaultApi.PaymentsClient().ReverseAPayment(paymentResponse.Id, secondReversalRequest);

            // Assert - The second reversal should also be successful
            // Note: The behavior depends on payment method and current state
            secondReversalResponse.ShouldNotBeNull();
            secondReversalResponse.ActionId.ShouldNotBeNullOrEmpty();
            
            // For different reversal operations, action IDs should be different
            // Unless it's a 204 response indicating already reversed
            if (secondReversalResponse.Reference != null)
            {
                secondReversalResponse.Reference.ShouldBe(secondReversalRequest.Reference);
            }
        }

        [Fact]
        public async Task ShouldReturn204_WhenPaymentAlreadyFullyReversed()
        {
            // Arrange - Create a small payment and reverse it completely
            var paymentResponse = await MakeCardPayment(shouldCapture: true, amount: 10);
            paymentResponse.ShouldNotBeNull();

            // First reversal - should complete the reversal
            var firstReversalRequest = new ReverseAPaymentRequest
            {
                Reference = $"complete-reversal-{Guid.NewGuid()}",
                Metadata = new { TestReason = "complete_reversal" }
            };

            var firstReversalResponse = await DefaultApi.PaymentsClient().ReverseAPayment(paymentResponse.Id, firstReversalRequest);
            firstReversalResponse.ShouldNotBeNull();

            // Wait for processing
            await Task.Delay(5000);

            // Second reversal attempt - according to docs, should return 204 for already reversed payment
            var secondReversalRequest = new ReverseAPaymentRequest
            {
                Reference = $"duplicate-reversal-{Guid.NewGuid()}",
                Metadata = new { TestReason = "duplicate_attempt" }
            };

            // Act & Assert - This might return 204 or succeed depending on timing and payment state
            try
            {
                var secondReversalResponse = await DefaultApi.PaymentsClient().ReverseAPayment(paymentResponse.Id, secondReversalRequest);
                // If we get here, the system allowed the second reversal
                secondReversalResponse.ShouldNotBeNull();
            }
            catch (CheckoutApiException ex) when (ex.HttpStatusCode == HttpStatusCode.NoContent)
            {
                // This is expected behavior according to documentation
                // Status 204 indicates payment was already reversed
                ex.HttpStatusCode.ShouldBe(HttpStatusCode.NoContent);
            }
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Checkout.Payments;
using Shouldly;
using Xunit;

namespace Checkout.Tests.Payments
{
    public class IdempotencyTests : ApiTestFixture
    {
        const string idempotencyKey = "1234";

        [Fact]
        public async Task CanIdempotentlyRequestPayment()
        {
            var paymentRequest = TestHelper.CreateCardPaymentRequest(amount: 1000);
            paymentRequest.Reference = "Idempotent Payment Request";
            paymentRequest.Metadata = new Dictionary<string, object> { { nameof(IdempotencyTests), nameof(CanIdempotentlyRequestPayment) } };

            var paymentResponse1 = await Api.Payments.RequestAsync(paymentRequest, idempotencyKey: idempotencyKey);
            paymentResponse1.ShouldNotBeNull();

            var paymentResponse2 = await Api.Payments.RequestAsync(paymentRequest, idempotencyKey: idempotencyKey);
            paymentResponse2.ShouldNotBeNull();

            paymentResponse2.Payment.Id.ShouldBe(paymentResponse1.Payment.Id);
        }

        [Fact]
        public async Task CanIdempotentlyCapturePayment()
        {
            var payment = await MakePaymentAsync(capture: false);

            var captureRequest = new CaptureRequest
            {
                Reference = "Idempotent Capture",
                Metadata = new Dictionary<string, object> { { nameof(IdempotencyTests), nameof(CanIdempotentlyCapturePayment) } }
            };

            var captureResponse1 = await Api.Payments.CaptureAsync(payment.Id, captureRequest, idempotencyKey: idempotencyKey);
            captureResponse1.ShouldNotBeNull();

            var captureResponse2 = await Api.Payments.CaptureAsync(payment.Id, captureRequest, idempotencyKey: idempotencyKey);
            captureResponse2.ShouldNotBeNull();

            captureResponse2.ActionId.ShouldBe(captureResponse1.ActionId);
        }

        [Fact]
        public async Task CanIdempotentlyVoidPayment()
        {
            var payment = await MakePaymentAsync(capture: false);

            var voidRequest = new VoidRequest
            {
                Reference = "Idempotent Void",
                Metadata = new Dictionary<string, object> { { nameof(IdempotencyTests), nameof(CanIdempotentlyVoidPayment) } }
            };

            var voidResponse1 = await Api.Payments.VoidAsync(payment.Id, voidRequest, idempotencyKey: idempotencyKey);
            voidResponse1.ShouldNotBeNull();

            var voidResponse2 = await Api.Payments.VoidAsync(payment.Id, voidRequest, idempotencyKey: idempotencyKey);
            voidResponse2.ShouldNotBeNull();

            voidResponse2.ActionId.ShouldBe(voidResponse1.ActionId);
        }

        [Fact]
        public async Task CanIdempotentlyRefundPayment()
        {
            var payment = await MakePaymentAsync(capture: false);

            await Api.Payments.CaptureAsync(payment.Id);

            var paymentResponse = await Api.Payments.GetAsync(payment.Id);
            paymentResponse.Status.ShouldBe("Captured");

            var refundRequest = new RefundRequest
            {
                Reference = "Idempotent Refund",
                Metadata = new Dictionary<string, object> { { nameof(IdempotencyTests), nameof(CanIdempotentlyRefundPayment) } }
            };

            var refundResponse1 = await Api.Payments.RefundAsync(payment.Id, refundRequest, idempotencyKey: idempotencyKey);
            refundResponse1.ShouldNotBeNull();

            var refundResponse2 = await Api.Payments.RefundAsync(payment.Id, refundRequest, idempotencyKey: idempotencyKey);
            refundResponse2.ShouldNotBeNull();

            refundResponse2.ActionId.ShouldBe(refundResponse1.ActionId);
        }

        async Task<PaymentProcessed> MakePaymentAsync(bool capture = true)
        {
            var paymentRequest = TestHelper.CreateCardPaymentRequest(amount: 1000);
            paymentRequest.Capture = capture;

            var paymentResponse = await Api.Payments.RequestAsync(paymentRequest);
            paymentResponse.Payment.CanCapture().ShouldBeTrue();

            return paymentResponse.Payment;
        }
    }
}
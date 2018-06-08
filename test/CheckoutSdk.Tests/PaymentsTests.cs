using System;
using System.Net;
using System.Threading.Tasks;
using Checkout.Payments;
using NSpec;
using Serilog;
using Shouldly;

namespace Checkout.Tests
{
    class describe_payments : ApiTest
    {
        private PaymentRequest<CardSource> paymentRequest;
        private PaymentResponse<CardSourceResponse> apiResponse;

        void describe_card_payments()
        {
            before = () => paymentRequest = CreateCardPaymentRequest();

            actAsync = async () => apiResponse = await Api.Payments.RequestAsync(paymentRequest);

            context["given non-3ds"] = () =>
            {
                before = () => paymentRequest.ThreeDs = false;

                it["returns processed payment details"] = () =>
                {
                    var payment = apiResponse.Payment;

                    payment.ShouldNotBeNull();
                    payment.Approved.ShouldBeTrue();
                    payment.Id.ShouldNotBeNullOrEmpty();
                    payment.ActionId.ShouldNotBeNullOrEmpty();
                    payment.Amount.ShouldBe(paymentRequest.Amount.Value);
                    payment.Currency.ShouldBe(paymentRequest.Currency);
                    payment.Reference.ShouldBe(paymentRequest.Reference);
                    payment.Customer.ShouldNotBeNull();
                    payment.Customer.Id.ShouldNotBeNullOrEmpty();
                    payment.Customer.Email.ShouldNotBeNullOrEmpty();
                    payment.CanCapture().ShouldBeTrue();
                    payment.CanVoid().ShouldBeTrue();
                };
            };

            context["given 3ds"] = () =>
            {
                before = () => paymentRequest.ThreeDs = true;

                it["returns pending payment details"] = () =>
                {
                    apiResponse.IsPending.ShouldBe(true);
                    var pending = apiResponse.Pending;

                    pending.ShouldNotBeNull();

                    pending.Id.ShouldNotBeNullOrEmpty();
                    pending.Reference.ShouldBe(paymentRequest.Reference);
                    pending.Customer.ShouldNotBeNull();
                    pending.Customer.Id.ShouldNotBeNullOrEmpty();
                    pending.Customer.Email.ShouldBe(paymentRequest.Customer.Email);
                    pending.ThreeDs.ShouldNotBeNull();
                    pending.ThreeDs.Downgraded.ShouldBe(false);
                    pending.ThreeDs.Enrolled.ShouldNotBeNullOrEmpty();
                    pending.RequiresRedirect().ShouldBe(true);
                    pending.GetRedirectLink().ShouldNotBeNull();
                };
            };
        }
        
        async Task it_can_capture_payment()
        {
            // Auth
            var paymentRequest = CreateCardPaymentRequest();
            var paymentResponse = await Api.Payments.RequestAsync(paymentRequest);
            paymentResponse.Payment.CanCapture().ShouldBe(true);

            CaptureRequest captureRequest = new CaptureRequest
            {
                Reference = Guid.NewGuid().ToString()
            };

            // Capture
            var captureResponse = await Api.Payments.CaptureAsync(paymentResponse.Payment.Id, captureRequest);

            captureResponse.ActionId.ShouldNotBeNullOrEmpty();
            captureResponse.Reference.ShouldBe(captureRequest.Reference);
        }

        async Task it_can_void_payment()
        {
            // Auth
            var paymentRequest = CreateCardPaymentRequest();
            var paymentResponse = await Api.Payments.RequestAsync(paymentRequest);
            paymentResponse.Payment.CanVoid().ShouldBe(true);

            VoidRequest voidRequest = new VoidRequest
            {
                Reference = Guid.NewGuid().ToString()
            };

            // Void Auth
            var voidResponse = await Api.Payments.VoidAsync(paymentResponse.Payment.Id, voidRequest);

            voidResponse.ActionId.ShouldNotBeNullOrEmpty();
            voidResponse.Reference.ShouldBe(voidRequest.Reference);
        }

        async Task it_can_refund_payment()
        {
            // Auth
            var paymentRequest = CreateCardPaymentRequest();
            var paymentResponse = await Api.Payments.RequestAsync(paymentRequest);
            paymentResponse.Payment.CanCapture().ShouldBe(true);

            // Capture
            var captureResponse = await Api.Payments.CaptureAsync(paymentResponse.Payment.Id);

            var refundRequest = new RefundRequest
            {
                Reference = Guid.NewGuid().ToString()
            };

            // Refund

            var refundResponse = await Api.Payments.RefundAsync(paymentResponse.Payment.Id, refundRequest);

            refundResponse.ActionId.ShouldNotBeNullOrEmpty();
            refundResponse.Reference.ShouldBe(refundRequest.Reference);
        }

        PaymentRequest<CardSource> CreateCardPaymentRequest()
        {
            return new PaymentRequest<CardSource>(
                    new CardSource(TestCard.Visa.Number, TestCard.Visa.ExpiryMonth, TestCard.Visa.ExpiryYear),
                    Currency.GBP,
                    100
                )
                {
                    Capture = false,
                    Customer = new Customer { Email = TestHelper.GenerateRandomEmail() },
                    Reference = Guid.NewGuid().ToString()
                };
        }
    }
}
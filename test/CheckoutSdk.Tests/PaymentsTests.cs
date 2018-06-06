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
        private ApiResponse<PaymentResponse<CardSourceResponse>> apiResponse;

        void describe_card_payments()
        {
            before = () => paymentRequest = CreateCardPaymentRequest();

            actAsync = async () => apiResponse = await Api.Payments.RequestAsync(paymentRequest);

            context["given non-3ds"] = () =>
            {
                before = () => paymentRequest.ThreeDs = false;

                it["returns processed payment details"] = () =>
                {
                    apiResponse.StatusCode.ShouldBe(HttpStatusCode.Created);
                    var payment = apiResponse.Result?.Payment;

                    payment.ShouldNotBeNull();
                    apiResponse.Error.ShouldBeNull();
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
                    apiResponse.StatusCode.ShouldBe(HttpStatusCode.Accepted);
                    apiResponse.Result.IsPending.ShouldBe(true);
                    var pending = apiResponse.Result?.Pending;

                    pending.ShouldNotBeNull();
                    apiResponse.Error.ShouldBeNull();

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
            paymentResponse.Result.Payment.CanCapture().ShouldBe(true);

            CaptureRequest captureRequest = new CaptureRequest
            {
                Reference = Guid.NewGuid().ToString()
            };

            // Capture
            var captureResponse = await Api.Payments.CaptureAsync(paymentResponse.Result.Payment.Id, captureRequest);

            captureResponse.StatusCode.ShouldBe(HttpStatusCode.Accepted);
            captureResponse.Result.ActionId.ShouldNotBeNullOrEmpty();
            captureResponse.Result.Reference.ShouldBe(captureRequest.Reference);
        }

        async Task it_can_void_payment()
        {
            // Auth
            var paymentRequest = CreateCardPaymentRequest();
            var paymentResponse = await Api.Payments.RequestAsync(paymentRequest);
            paymentResponse.Result.Payment.CanVoid().ShouldBe(true);

            VoidRequest voidRequest = new VoidRequest
            {
                Reference = Guid.NewGuid().ToString()
            };

            // Void Auth
            var voidResponse = await Api.Payments.VoidAsync(paymentResponse.Result.Payment.Id, voidRequest);

            voidResponse.StatusCode.ShouldBe(HttpStatusCode.Accepted);
            voidResponse.Result.ActionId.ShouldNotBeNullOrEmpty();
            voidResponse.Result.Reference.ShouldBe(voidRequest.Reference);
        }

        async Task it_can_refund_payment()
        {
            // Auth
            var paymentRequest = CreateCardPaymentRequest();
            var paymentResponse = await Api.Payments.RequestAsync(paymentRequest);
            paymentResponse.Result.Payment.CanCapture().ShouldBe(true);

            // Capture
            var captureResponse = await Api.Payments.CaptureAsync(paymentResponse.Result.Payment.Id);

            var refundRequest = new RefundRequest
            {
                Reference = Guid.NewGuid().ToString()
            };

            // Refund

            var refundResponse = await Api.Payments.RefundAsync(paymentResponse.Result.Payment.Id, refundRequest);

            refundResponse.StatusCode.ShouldBe(HttpStatusCode.Accepted);
            refundResponse.Result.ActionId.ShouldNotBeNullOrEmpty();
            refundResponse.Result.Reference.ShouldBe(refundRequest.Reference);
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
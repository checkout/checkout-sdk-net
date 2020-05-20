using Shouldly;
using System;
using System.Threading.Tasks;
using Checkout.Payments;
using Xunit;
using Xunit.Abstractions;

namespace Checkout.Tests.Payments
{
    public class CardSourcePaymentsTests : IClassFixture<ApiTestFixture>
    {
        private readonly ICheckoutApi _api;

        public CardSourcePaymentsTests(ApiTestFixture fixture, ITestOutputHelper outputHelper)
        {
            fixture.CaptureLogsInTestOutput(outputHelper);
            _api = fixture.Api;
        }

        [Fact]
        public async Task CanRequestNonThreeDsCardPayment()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            paymentRequest.ThreeDS = false;

            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);
            
            paymentResponse.Payment.ShouldNotBeNull();
            paymentResponse.Payment.Approved.ShouldBeTrue();
            paymentResponse.Payment.Id.ShouldNotBeNullOrEmpty();
            paymentResponse.Payment.ActionId.ShouldNotBeNullOrEmpty();
            paymentResponse.Payment.Amount.ShouldBe(paymentRequest.Amount.Value);
            paymentResponse.Payment.Currency.ShouldBe(paymentRequest.Currency);
            paymentResponse.Payment.Reference.ShouldBe(paymentRequest.Reference);
            paymentResponse.Payment.Customer.ShouldNotBeNull();
            paymentResponse.Payment.Customer.Id.ShouldNotBeNullOrEmpty();
            paymentResponse.Payment.Customer.Email.ShouldNotBeNullOrEmpty();
            paymentResponse.Payment.CanCapture().ShouldBeTrue();
            paymentResponse.Payment.CanVoid().ShouldBeTrue();
            paymentResponse.Payment.Source.AsCard().ShouldNotBeNull();
            paymentResponse.Payment.Processing.ShouldNotBeNull();
            paymentResponse.Payment.Processing.AcquirerTransactionId.ShouldNotBeNullOrWhiteSpace();
            paymentResponse.Payment.Processing.RetrievalReferenceNumber.ShouldNotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task CanRequestThreeDsCardPayment()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest();
            paymentRequest.ThreeDS = true;

            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(paymentRequest);

            paymentResponse.IsPending.ShouldBe(true);
            var pending = paymentResponse.Pending;

            pending.ShouldNotBeNull();

            pending.Id.ShouldNotBeNullOrEmpty();
            pending.Reference.ShouldBe(paymentRequest.Reference);
            pending.Customer.ShouldNotBeNull();
            pending.Customer.Id.ShouldNotBeNullOrEmpty();
            pending.Customer.Email.ShouldBe(paymentRequest.Customer.Email);
            pending.ThreeDS.ShouldNotBeNull();
            pending.ThreeDS.Downgraded.ShouldBe(false);
            pending.ThreeDS.Enrolled.ShouldNotBeNullOrEmpty();
            pending.RequiresRedirect().ShouldBe(true);
            pending.GetRedirectLink().ShouldNotBeNull();
        }

        [Fact]
        public async Task CanVoidPayment()
        {
            // Auth
            var paymentRequest = TestHelper.CreateCardPaymentRequest();
            var paymentResponse = await _api.Payments.RequestAsync(paymentRequest);
            paymentResponse.Payment.CanVoid().ShouldBe(true);

            VoidRequest voidRequest = new VoidRequest
            {
                Reference = Guid.NewGuid().ToString()
            };

            // Void Auth
            var voidResponse = await _api.Payments.VoidAsync(paymentResponse.Payment.Id, voidRequest);

            voidResponse.ActionId.ShouldNotBeNullOrEmpty();
            voidResponse.Reference.ShouldBe(voidRequest.Reference);
        }

        [Fact]
        public async Task CanRefundPayment()
        {
            // Auth
            var paymentRequest = TestHelper.CreateCardPaymentRequest();
            var paymentResponse = await _api.Payments.RequestAsync(paymentRequest);
            paymentResponse.Payment.CanCapture().ShouldBe(true);

            // Capture
            await _api.Payments.CaptureAsync(paymentResponse.Payment.Id);

            var refundRequest = new RefundRequest
            {
                Reference = Guid.NewGuid().ToString()
            };

            // Refund

            var refundResponse = await _api.Payments.RefundAsync(paymentResponse.Payment.Id, refundRequest);

            refundResponse.ActionId.ShouldNotBeNullOrEmpty();
            refundResponse.Reference.ShouldBe(refundRequest.Reference);
        }
    }
}

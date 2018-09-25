using Checkout.Payments;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Checkout.Tests.Payments
{
    public class TokenSourcePaymentsTests : IClassFixture<ApiTestFixture>
    {
        private readonly ICheckoutApi _api;

        public TokenSourcePaymentsTests(ApiTestFixture fixture, ITestOutputHelper outputHelper)
        {
            fixture.CaptureLogsInTestOutput(outputHelper);
            _api = fixture.Api;
        }

        [Fact]
        public async Task CanRequestNonThreeDsCardPayment()
        {
            var cardTokenRequest = TestHelper.CreateCardTokenRequest();
            var cardTokenResponse = await _api.Tokens.RequestAsync(cardTokenRequest);
            var paymentRequest = TestHelper.CreateTokenPaymentRequest(cardTokenResponse.Token);
            paymentRequest.ThreeDs = false;

            var paymentResponse = await _api.Payments.RequestAsync(paymentRequest);

            paymentResponse.Payment.ShouldNotBeNull();
            paymentResponse.Payment.Approved.ShouldBeTrue();
            paymentResponse.Payment.Id.ShouldNotBeNullOrEmpty();
            paymentResponse.Payment.ActionId.ShouldNotBeNullOrEmpty();
            paymentResponse.Payment.Amount.ShouldBe(paymentRequest.Amount.Value);
            paymentResponse.Payment.Currency.ShouldBe(paymentRequest.Currency);
            paymentResponse.Payment.Reference.ShouldBe(paymentRequest.Reference);
            paymentResponse.Payment.CanCapture().ShouldBeTrue();
            paymentResponse.Payment.CanVoid().ShouldBeTrue();
            paymentResponse.Payment.Source.AsCardSource().ShouldNotBeNull();
        }

        [Fact]
        public async Task CanRequestThreeDsCardPayment()
        {
            var cardTokenRequest = TestHelper.CreateCardTokenRequest();
            var cardTokenResponse = await _api.Tokens.RequestAsync(cardTokenRequest);
            var paymentRequest = TestHelper.CreateTokenPaymentRequest(cardTokenResponse.Token);
            paymentRequest.ThreeDs = true;

            var apiResponse = await _api.Payments.RequestAsync(paymentRequest);

            apiResponse.IsPending.ShouldBe(true);
            var pending = apiResponse.Pending;

            pending.ShouldNotBeNull();

            pending.Id.ShouldNotBeNullOrEmpty();
            pending.Reference.ShouldBe(paymentRequest.Reference);
            pending.ThreeDs.ShouldNotBeNull();
            pending.ThreeDs.Downgraded.ShouldBe(false);
            pending.ThreeDs.Enrolled.ShouldNotBeNullOrEmpty();
            pending.RequiresRedirect().ShouldBe(true);
            pending.GetRedirectLink().ShouldNotBeNull();
        }

        [Fact]
        public async Task CanCapturePayment()
        {
            var cardTokenRequest = TestHelper.CreateCardTokenRequest();
            var cardTokenResponse = await _api.Tokens.RequestAsync(cardTokenRequest);
            var paymentRequest = TestHelper.CreateTokenPaymentRequest(cardTokenResponse.Token);
            var paymentResponse = await _api.Payments.RequestAsync(paymentRequest);
            paymentResponse.Payment.CanCapture().ShouldBe(true);

            var captureRequest = new CaptureRequest
            {
                Reference = Guid.NewGuid().ToString()
            };

            var captureResponse = await _api.Payments.CaptureAsync(paymentResponse.Payment.Id, captureRequest);

            captureResponse.ActionId.ShouldNotBeNullOrEmpty();
            captureResponse.Reference.ShouldBe(captureRequest.Reference);
        }

        [Fact]
        public async Task CanVoidPayment()
        {
            var paymentRequest = TestHelper.CreateCardPaymentRequest();
            var paymentResponse = await _api.Payments.RequestAsync(paymentRequest);
            paymentResponse.Payment.CanVoid().ShouldBe(true);

            var voidRequest = new VoidRequest
            {
                Reference = Guid.NewGuid().ToString()
            };

            var voidResponse = await _api.Payments.VoidAsync(paymentResponse.Payment.Id, voidRequest);

            voidResponse.ActionId.ShouldNotBeNullOrEmpty();
            voidResponse.Reference.ShouldBe(voidRequest.Reference);
        }

        [Fact]
        public async Task CanRefundPayment()
        {
            var cardTokenRequest = TestHelper.CreateCardTokenRequest();
            var cardTokenResponse = await _api.Tokens.RequestAsync(cardTokenRequest);
            var paymentRequest = TestHelper.CreateTokenPaymentRequest(cardTokenResponse.Token);
            var paymentResponse = await _api.Payments.RequestAsync(paymentRequest);
            paymentResponse.Payment.CanCapture().ShouldBe(true);

            await _api.Payments.CaptureAsync(paymentResponse.Payment.Id);

            var refundRequest = new RefundRequest
            {
                Reference = Guid.NewGuid().ToString()
            };

            var refundResponse = await _api.Payments.RefundAsync(paymentResponse.Payment.Id, refundRequest);

            refundResponse.ActionId.ShouldNotBeNullOrEmpty();
            refundResponse.Reference.ShouldBe(refundRequest.Reference);
        }        
    }
}
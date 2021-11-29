using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Checkout.Payments.Four
{
    public class CapturePaymentsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact]
        private async Task ShouldFullCaptureCardPayment()
        {
            var paymentResponse = await MakeCardPayment();

            var captureRequest = new CaptureRequest
            {
                Reference = Guid.NewGuid().ToString(),
                Amount = 10
            };

            captureRequest.Metadata.Add("CapturePaymentsIntegrationTest", "ShouldFullCaptureCardPayment");

            var response = await FourApi.PaymentsClient().CapturePayment(paymentResponse.Id, captureRequest);

            response.ShouldNotBeNull();
            response.Reference.ShouldNotBeNullOrEmpty();
            response.ActionId.ShouldNotBeNullOrEmpty();

            await Nap();

            var payment = await FourApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);
            //Balances
            payment.Balances.TotalAuthorized.ShouldBe(paymentResponse.Amount);
            payment.Balances.TotalCaptured.ShouldBe(paymentResponse.Amount);
            payment.Balances.TotalRefunded.ShouldBe(0);
            payment.Balances.TotalVoided.ShouldBe(0);
            payment.Balances.AvailableToCapture.ShouldBe(0);
            payment.Balances.AvailableToRefund.ShouldBe(paymentResponse.Amount);
            payment.Balances.AvailableToVoid.ShouldBe(0);
        }

        [Fact]
        private async Task ShouldPartiallyCaptureCardPayment()
        {
            var paymentResponse = await MakeCardPayment();

            var captureRequest = new CaptureRequest
            {
                Reference = Guid.NewGuid().ToString(),
                Amount = paymentResponse.Amount / 2
            };

            captureRequest.Metadata.Add("CapturePaymentsIntegrationTest", "ShouldFullCaptureCardPayment");

            var response = await FourApi.PaymentsClient().CapturePayment(paymentResponse.Id, captureRequest);

            response.ShouldNotBeNull();
            response.Reference.ShouldNotBeNullOrEmpty();
            response.ActionId.ShouldNotBeNullOrEmpty();

            await Nap();

            var payment = await FourApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);
            //Balances
            payment.Balances.TotalAuthorized.ShouldBe(paymentResponse.Amount);
            payment.Balances.TotalCaptured.ShouldBe(paymentResponse.Amount / 2);
            payment.Balances.TotalRefunded.ShouldBe(0);
            payment.Balances.TotalVoided.ShouldBe(0);
            payment.Balances.AvailableToCapture.ShouldBe(0);
            payment.Balances.AvailableToRefund.ShouldBe(paymentResponse.Amount / 2);
            payment.Balances.AvailableToVoid.ShouldBe(0);
        }

        [Fact]
        private async Task ShouldCaptureCardPaymentIdempotently()
        {
            var paymentResponse = await MakeCardPayment();

            var captureRequest = new CaptureRequest
            {
                Reference = Guid.NewGuid().ToString()
            };

            captureRequest.Metadata.Add("CapturePaymentsIntegrationTest", "ShouldFullCaptureCardPayment");

            var capture1 = await FourApi.PaymentsClient()
                .CapturePayment(paymentResponse.Id, captureRequest, IdempotencyKey);
            capture1.ShouldNotBeNull();

            var capture2 = await FourApi.PaymentsClient()
                .CapturePayment(paymentResponse.Id, captureRequest, IdempotencyKey);
            capture2.ShouldNotBeNull();

            capture1.ActionId.ShouldBe(capture2.ActionId);
        }
    }
}
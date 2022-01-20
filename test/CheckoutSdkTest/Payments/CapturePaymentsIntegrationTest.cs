using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments
{
    public class CapturePaymentsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact]
        private async Task ShouldFullCaptureCardPayment()
        {
            var paymentResponse = await MakeCardPayment();

            var captureRequest = new CaptureRequest {Reference = Guid.NewGuid().ToString()};

            captureRequest.Metadata.Add("CapturePaymentsIntegrationTest", "ShouldFullCaptureCardPayment");

            var response = await DefaultApi.PaymentsClient().CapturePayment(paymentResponse.Id, captureRequest);

            response.ShouldNotBeNull();
            response.Reference.ShouldNotBeNullOrEmpty();
            response.ActionId.ShouldNotBeNullOrEmpty();
        }

        [Fact(Skip = "unstable")]
        private async Task ShouldPartiallyCaptureCardPayment()
        {
            var paymentResponse = await MakeCardPayment();

            var captureRequest = new CaptureRequest {Reference = Guid.NewGuid().ToString(), Amount = 5};

            captureRequest.Metadata.Add("CapturePaymentsIntegrationTest", "ShouldFullCaptureCardPayment");

            var response = await DefaultApi.PaymentsClient().CapturePayment(paymentResponse.Id, captureRequest);

            response.ShouldNotBeNull();
            response.Reference.ShouldNotBeNullOrEmpty();
            response.ActionId.ShouldNotBeNullOrEmpty();
        }

        [Fact]
        private async Task ShouldCaptureCardPaymentIdempotently()
        {
            var paymentResponse = await MakeCardPayment();

            var captureRequest = new CaptureRequest {Reference = Guid.NewGuid().ToString()};

            captureRequest.Metadata.Add("CapturePaymentsIntegrationTest", "ShouldFullCaptureCardPayment");

            var capture1 = await DefaultApi.PaymentsClient()
                .CapturePayment(paymentResponse.Id, captureRequest, IdempotencyKey);
            capture1.ShouldNotBeNull();

            var capture2 = await DefaultApi.PaymentsClient()
                .CapturePayment(paymentResponse.Id, captureRequest, IdempotencyKey);
            capture2.ShouldNotBeNull();

            capture1.ActionId.ShouldBe(capture2.ActionId);
        }
    }
}
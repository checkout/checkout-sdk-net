using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments.Previous
{
    public class CapturePaymentsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact(Skip = "unavailable")]
        private async Task ShouldFullCaptureCardPayment()
        {
            var paymentResponse = await MakeCardPayment();

            var captureRequest = new CaptureRequest {Reference = Guid.NewGuid().ToString()};

            captureRequest.Metadata.Add("CapturePaymentsIntegrationTest", "ShouldFullCaptureCardPayment");

            var response = await Retriable(async () =>
                await PreviousApi.PaymentsClient().CapturePayment(paymentResponse.Id, captureRequest));

            response.ShouldNotBeNull();
            response.Reference.ShouldNotBeNullOrEmpty();
            response.ActionId.ShouldNotBeNullOrEmpty();
        }

        [Fact(Skip = "unavailable")]
        private async Task ShouldPartiallyCaptureCardPayment()
        {
            var paymentResponse = await MakeCardPayment();

            var captureRequest = new CaptureRequest {Reference = Guid.NewGuid().ToString(), Amount = 5};

            captureRequest.Metadata.Add("CapturePaymentsIntegrationTest", "ShouldFullCaptureCardPayment");

            var response = await Retriable(async () =>
                await PreviousApi.PaymentsClient().CapturePayment(paymentResponse.Id, captureRequest));

            response.ShouldNotBeNull();
            response.Reference.ShouldNotBeNullOrEmpty();
            response.ActionId.ShouldNotBeNullOrEmpty();
        }
        
        [Fact(Skip = "unavailable")]
        private async Task ShouldFullCaptureCardPaymentWithoutRequest()
        {
            var paymentResponse = await MakeCardPayment();

            var response = await Retriable(async () =>
                await PreviousApi.PaymentsClient().CapturePayment(paymentResponse.Id));

            response.ShouldNotBeNull();
        }

        [Fact(Skip = "unavailable")]
        private async Task ShouldCaptureCardPaymentIdempotently()
        {
            var paymentResponse = await MakeCardPayment();

            var captureRequest = new CaptureRequest {Reference = Guid.NewGuid().ToString()};

            captureRequest.Metadata.Add("CapturePaymentsIntegrationTest", "ShouldFullCaptureCardPayment");

            var capture1 = await PreviousApi.PaymentsClient()
                .CapturePayment(paymentResponse.Id, captureRequest, IdempotencyKey);
            capture1.ShouldNotBeNull();

            var capture2 = await PreviousApi.PaymentsClient()
                .CapturePayment(paymentResponse.Id, captureRequest, IdempotencyKey);
            capture2.ShouldNotBeNull();

            capture1.ActionId.ShouldBe(capture2.ActionId);
        }
    }
}
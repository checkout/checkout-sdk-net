using Checkout.Payments.Four.Response;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments.Four
{
    public class CapturePaymentsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact]
        private async Task ShouldFullCaptureCardPayment()
        {
            var paymentResponse = await MakeCardPayment();

            var captureRequest = new CaptureRequest {Reference = Guid.NewGuid().ToString(), Amount = 10};

            captureRequest.Metadata.Add("CapturePaymentsIntegrationTest", "ShouldFullCaptureCardPayment");

            var response = await FourApi.PaymentsClient().CapturePayment(paymentResponse.Id, captureRequest);

            response.ShouldNotBeNull();
            response.Reference.ShouldNotBeNullOrEmpty();
            response.ActionId.ShouldNotBeNullOrEmpty();

            var payment = await Retriable(async () =>
                await FourApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id), TotalCapturedIs10);

            //Balances
            payment.Balances.TotalAuthorized.ShouldBe(paymentResponse.Amount);
            payment.Balances.TotalCaptured.ShouldBe(paymentResponse.Amount);
            payment.Balances.TotalRefunded.ShouldBe(0);
            payment.Balances.TotalVoided.ShouldBe(0);
            payment.Balances.AvailableToCapture.ShouldBe(0);
            payment.Balances.AvailableToRefund.ShouldBe(paymentResponse.Amount);
            payment.Balances.AvailableToVoid.ShouldBe(0);
        }

        private static bool TotalCapturedIs10(GetPaymentResponse obj)
        {
            return obj.Balances.TotalCaptured == 10;
        }

        [Fact]
        private async Task ShouldPartiallyCaptureCardPayment()
        {
            var paymentResponse = await MakeCardPayment();

            var captureRequest = new CaptureRequest
            {
                Reference = Guid.NewGuid().ToString(), Amount = paymentResponse.Amount / 2
            };

            captureRequest.Metadata.Add("CapturePaymentsIntegrationTest", "ShouldFullCaptureCardPayment");

            var response = await FourApi.PaymentsClient().CapturePayment(paymentResponse.Id, captureRequest);

            response.ShouldNotBeNull();
            response.Reference.ShouldNotBeNullOrEmpty();
            response.ActionId.ShouldNotBeNullOrEmpty();

            var payment = await Retriable(async () =>
                await FourApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id), TotalCapturedIs5);

            //Balances
            payment.Balances.TotalAuthorized.ShouldBe(paymentResponse.Amount);
            payment.Balances.TotalCaptured.ShouldBe(paymentResponse.Amount / 2);
            payment.Balances.TotalRefunded.ShouldBe(0);
            payment.Balances.TotalVoided.ShouldBe(0);
            payment.Balances.AvailableToCapture.ShouldBe(0);
            payment.Balances.AvailableToRefund.ShouldBe(paymentResponse.Amount / 2);
            payment.Balances.AvailableToVoid.ShouldBe(0);
        }

        private static bool TotalCapturedIs5(GetPaymentResponse obj)
        {
            return obj.Balances.TotalCaptured == 5;
        }

        [Fact]
        private async Task ShouldCaptureCardPaymentIdempotently()
        {
            var paymentResponse = await MakeCardPayment();

            var captureRequest = new CaptureRequest {Reference = Guid.NewGuid().ToString()};

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
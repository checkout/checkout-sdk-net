using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments
{
    public class RefundPaymentsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact]
        private async Task ShouldRefundCardPayment()
        {
            var paymentResponse = await MakeCardPayment(true);

            var refundRequest = new RefundRequest
            {
                Reference = Guid.NewGuid().ToString(), Amount = paymentResponse.Amount
            };

            var response = await Retriable(async () =>
                await DefaultApi.PaymentsClient().RefundPayment(paymentResponse.Id, refundRequest));

            response.ShouldNotBeNull();
            response.ActionId.ShouldNotBeNullOrEmpty();
            response.Reference.ShouldNotBeNullOrEmpty();
            response.GetLink("payment").ShouldNotBeNull();

            var payment = await Retriable(async () =>
                await DefaultApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id));
            //Balances
            payment.Balances.TotalAuthorized.ShouldBe(paymentResponse.Amount);
            payment.Balances.TotalCaptured.ShouldBe(paymentResponse.Amount);
            payment.Balances.TotalRefunded.ShouldBe(paymentResponse.Amount);
            payment.Balances.TotalVoided.ShouldBe(0);
            payment.Balances.AvailableToCapture.ShouldBe(0);
            payment.Balances.AvailableToRefund.ShouldBe(0);
            payment.Balances.AvailableToVoid.ShouldBe(0);
        }

        [Fact]
        private async Task ShouldRefundPartiallyCardPayment()
        {
            var paymentResponse = await MakeCardPayment(true);

            var refundRequest = new RefundRequest
            {
                Reference = Guid.NewGuid().ToString(), Amount = paymentResponse.Amount / 2
            };

            var response = await Retriable(async () =>
                await DefaultApi.PaymentsClient().RefundPayment(paymentResponse.Id, refundRequest));

            response.ShouldNotBeNull();
            response.ActionId.ShouldNotBeNullOrEmpty();
            response.Reference.ShouldNotBeNullOrEmpty();
            response.GetLink("payment").ShouldNotBeNull();

            var payment = await Retriable(async () =>
                await DefaultApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id));
            //Balances
            payment.Balances.TotalAuthorized.ShouldBe(paymentResponse.Amount);
            payment.Balances.TotalCaptured.ShouldBe(paymentResponse.Amount);
            payment.Balances.TotalRefunded.ShouldBe(paymentResponse.Amount / 2);
            payment.Balances.TotalVoided.ShouldBe(0);
            payment.Balances.AvailableToCapture.ShouldBe(0);
            payment.Balances.AvailableToRefund.ShouldBe(paymentResponse.Amount / 2);
            payment.Balances.AvailableToVoid.ShouldBe(0);
        }
    }
}
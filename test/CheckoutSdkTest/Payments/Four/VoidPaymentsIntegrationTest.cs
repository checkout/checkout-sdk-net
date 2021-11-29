using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Checkout.Payments.Four
{
    public class VoidPaymentsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact]
        private async Task ShouldFullVoidCardPayment()
        {
            var paymentResponse = await MakeCardPayment();

            var voidRequest = new VoidRequest
            {
                Reference = Guid.NewGuid().ToString()
            };

            var response = await FourApi.PaymentsClient().VoidPayment(paymentResponse.Id, voidRequest);

            response.ShouldNotBeNull();
            response.ActionId.ShouldNotBeNullOrEmpty();
            response.Reference.ShouldNotBeNullOrEmpty();
            response.GetLink("payment").ShouldNotBeNull();

            await Nap();

            var payment = await FourApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);
            //Balances
            payment.Balances.TotalAuthorized.ShouldBe(paymentResponse.Amount);
            payment.Balances.TotalCaptured.ShouldBe(0);
            payment.Balances.TotalRefunded.ShouldBe(0);
            payment.Balances.TotalVoided.ShouldBe(paymentResponse.Amount);
            payment.Balances.AvailableToCapture.ShouldBe(0);
            payment.Balances.AvailableToRefund.ShouldBe(0);
            payment.Balances.AvailableToVoid.ShouldBe(0);
        }

        [Fact]
        private async Task ShouldVoidCardPaymentIdempotently()
        {
            var paymentResponse = await MakeCardPayment();

            var voidRequest = new VoidRequest
            {
                Reference = Guid.NewGuid().ToString()
            };

            var response = await FourApi.PaymentsClient()
                .VoidPayment(paymentResponse.Id, voidRequest, IdempotencyKey);
            response.ShouldNotBeNull();

            var response2 = await FourApi.PaymentsClient()
                .VoidPayment(paymentResponse.Id, voidRequest, IdempotencyKey);
            response2.ShouldNotBeNull();

            response.ActionId.ShouldBe(response2.ActionId);
        }
    }
}
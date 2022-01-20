using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments
{
    public class RefundPaymentsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact(Skip = "unstable")]
        private async Task ShouldRefundCardPayment()
        {
            var paymentResponse = await MakeCardPayment(true);

            await Nap();

            var refundRequest = new RefundRequest {Reference = Guid.NewGuid().ToString()};

            var response = await DefaultApi.PaymentsClient().RefundPayment(paymentResponse.Id, refundRequest);

            response.ShouldNotBeNull();
            response.ActionId.ShouldNotBeNullOrEmpty();
            response.Reference.ShouldNotBeNullOrEmpty();
            response.GetLink("payment").ShouldNotBeNull();
        }

        [Fact(Skip = "unstable")]
        private async Task ShouldRefundCardPayment_Idempotently()
        {
            var paymentResponse = await MakeCardPayment(true);

            await Nap();

            var refundRequest = new RefundRequest {Reference = Guid.NewGuid().ToString(), Amount = 2};

            var response1 = await DefaultApi.PaymentsClient()
                .RefundPayment(paymentResponse.Id, refundRequest, IdempotencyKey);

            var refundRequest2 = new RefundRequest {Reference = Guid.NewGuid().ToString(), Amount = 2};

            var response2 = await DefaultApi.PaymentsClient()
                .RefundPayment(paymentResponse.Id, refundRequest2, IdempotencyKey);

            response1.ActionId.ShouldBe(response2.ActionId);
        }
    }
}
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments.Previous
{
    public class RefundPaymentsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact(Skip = "unavailable")]
        private async Task ShouldRefundCardPayment()
        {
            var paymentResponse = await MakeCardPayment(true);

            var refundRequest = new RefundRequest {Reference = Guid.NewGuid().ToString()};

            var response = await Retriable(async () =>
                await PreviousApi.PaymentsClient().RefundPayment(paymentResponse.Id, refundRequest));

            response.ShouldNotBeNull();
            response.ActionId.ShouldNotBeNullOrEmpty();
            response.Reference.ShouldNotBeNullOrEmpty();
            response.GetLink("payment").ShouldNotBeNull();
        }

        [Fact(Skip = "unavailable")]
        private async Task ShouldRefundCardPayment_Idempotently()
        {
            var paymentResponse = await MakeCardPayment(true);

            var refundRequest = new RefundRequest {Reference = Guid.NewGuid().ToString(), Amount = 2};

            var response1 = await Retriable(async () => await PreviousApi.PaymentsClient()
                .RefundPayment(paymentResponse.Id, refundRequest, IdempotencyKey));

            var refundRequest2 = new RefundRequest {Reference = Guid.NewGuid().ToString(), Amount = 2};

            var response2 = await Retriable(async () => await PreviousApi.PaymentsClient()
                .RefundPayment(paymentResponse.Id, refundRequest2, IdempotencyKey));

            response1.ActionId.ShouldBe(response2.ActionId);
        }
    }
}
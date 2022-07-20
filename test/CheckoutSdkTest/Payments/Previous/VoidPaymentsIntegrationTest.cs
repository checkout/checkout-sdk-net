using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments.Previous
{
    public class VoidPaymentsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact]
        private async Task ShouldVoidCardPayment()
        {
            var paymentResponse = await MakeCardPayment();

            var voidRequest = new VoidRequest {Reference = Guid.NewGuid().ToString()};

            var response = await PreviousApi.PaymentsClient().VoidPayment(paymentResponse.Id, voidRequest);

            response.ShouldNotBeNull();
            response.ActionId.ShouldNotBeNullOrEmpty();
            response.Reference.ShouldNotBeNullOrEmpty();
            response.GetLink("payment").ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldVoidCardPaymentIdempotently()
        {
            var paymentResponse = await MakeCardPayment();

            var voidRequest = new VoidRequest {Reference = Guid.NewGuid().ToString()};

            var response = await Retriable(async () =>
                await PreviousApi.PaymentsClient()
                    .VoidPayment(paymentResponse.Id, voidRequest, IdempotencyKey));

            response.ShouldNotBeNull();

            var response2 = await Retriable(async () =>
                await PreviousApi.PaymentsClient()
                    .VoidPayment(paymentResponse.Id, voidRequest, IdempotencyKey));
            response2.ShouldNotBeNull();

            response.ActionId.ShouldBe(response2.ActionId);
        }
    }
}
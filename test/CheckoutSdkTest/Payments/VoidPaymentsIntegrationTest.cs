using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments
{
    public class VoidPaymentsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact]
        private async Task ShouldVoidCardPayment()
        {
            var paymentResponse = await MakeCardPayment();

            var voidRequest = new VoidRequest {Reference = Guid.NewGuid().ToString()};

            var response = await DefaultApi.PaymentsClient().VoidPayment(paymentResponse.Id, voidRequest);

            response.ShouldNotBeNull();
            response.ActionId.ShouldNotBeNullOrEmpty();
            response.Reference.ShouldNotBeNullOrEmpty();
            response.GetLink("payment").ShouldNotBeNull();
        }

        [Fact(Skip = "unstable")]
        private async Task ShouldVoidCardPaymentIdempotently()
        {
            var paymentResponse = await MakeCardPayment();

            var voidRequest = new VoidRequest {Reference = Guid.NewGuid().ToString()};

            var response = await DefaultApi.PaymentsClient()
                .VoidPayment(paymentResponse.Id, voidRequest, IdempotencyKey);
            response.ShouldNotBeNull();

            var response2 = await DefaultApi.PaymentsClient()
                .VoidPayment(paymentResponse.Id, voidRequest, IdempotencyKey);
            response2.ShouldNotBeNull();

            response.ActionId.ShouldBe(response2.ActionId);
        }
    }
}
using Checkout.Payments.Previous.Response;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments.Previous
{
    public class GetPaymentsListIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact(Skip = "unavailable")]
        private async Task ShouldGetPaymentsList()
        {
            var paymentResponse = await MakeCardPayment(true);

            var query = new PaymentsQueryFilter { Limit = 100, Skip = 0, Reference = paymentResponse.Reference };
        
            PaymentsQueryResponse response = await Retriable(async () =>
                await PreviousApi.PaymentsClient().GetPaymentsList(query), ThereArePayments);
        
            response.ShouldNotBeNull();
            response.Limit.ShouldNotBeNull();
            response.Skip.ShouldNotBeNull();
            response.TotalCount.ShouldNotBeNull();
            if (response.TotalCount > 0)
            {
                var payment = response.Data[0];
                payment.ShouldNotBeNull();
                payment.Id.ShouldBe(paymentResponse.Id);
                payment.Reference.ShouldBe(paymentResponse.Reference);
                payment.Amount.ShouldBe(paymentResponse.Amount);
                payment.Currency.ShouldBe(paymentResponse.Currency);
            }
        }
    
        private static bool ThereArePayments(PaymentsQueryResponse obj)
        {
            return obj.TotalCount > 0;
        }
    }
}

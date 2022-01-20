using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments
{
    public class PaymentActionsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact(Skip = "unstable")]
        private async Task ShouldGetPaymentActions()
        {
            var paymentResponse = await MakeCardPayment(true);

            await Nap();

            var actions = await DefaultApi.PaymentsClient().GetPaymentActions(paymentResponse.Id);

            actions.ShouldNotBeNull();
            actions.Count.ShouldBe(2);

            foreach (var paymentAction in actions)
            {
                paymentAction.Amount.ShouldBe(10);
                paymentAction.Approved.ShouldBe(true);
                paymentAction.Links.ShouldNotBeNull();
                paymentAction.ProcessedOn.ShouldNotBeNull();
                paymentAction.Reference.ShouldNotBeNullOrEmpty();
                paymentAction.ResponseCode.ShouldNotBeNullOrEmpty();
                paymentAction.ResponseSummary.ShouldNotBeNullOrEmpty();
                paymentAction.Type.ShouldNotBeNull();
            }
        }
    }
}
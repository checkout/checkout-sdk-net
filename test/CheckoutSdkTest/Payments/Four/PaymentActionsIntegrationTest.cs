using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments.Four
{
    public class PaymentActionsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact]
        private async Task ShouldGetPaymentActions()
        {
            var paymentResponse = await MakeCardPayment(true);

            var actions = await Retriable(async () =>
                await FourApi.PaymentsClient().GetPaymentActions(paymentResponse.Id), ThereAreTwoPaymentActions);

            actions.ShouldNotBeNull();
            actions.Count.ShouldBe(2);

            foreach (var paymentAction in actions)
            {
                paymentAction.Amount.ShouldBe(10);
                paymentAction.Approved.ShouldBe(true);
                paymentAction.ProcessedOn.ShouldNotBeNull();
                paymentAction.Reference.ShouldNotBeNullOrEmpty();
                paymentAction.ResponseCode.ShouldNotBeNullOrEmpty();
                paymentAction.ResponseSummary.ShouldNotBeNullOrEmpty();
                paymentAction.Type.ShouldNotBeNull();
            }
        }

        private static bool ThereAreTwoPaymentActions(IList<PaymentAction> obj)
        {
            return obj.Count == 2;
        }
    }
}
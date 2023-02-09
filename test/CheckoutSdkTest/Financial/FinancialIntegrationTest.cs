using Castle.Core.Internal;
using Checkout.Common;
using Checkout.Payments;
using Checkout.Payments.Request;
using Checkout.Payments.Request.Source;
using Checkout.Payments.Response;
using Checkout.Payments.Sender;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace Checkout.Financial
{
    public class FinancialIntegrationTest : AbstractPaymentsIntegrationTest
    {
        public FinancialIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact]
        private async Task ShouldQueryFinancialActions()
        {
            //Make the payment
            var payment = await MakeCardPayment(true, 100L);
            payment.ShouldNotBeNull();
            
            var query = new FinancialActionsQueryFilter { PaymentId = payment.Id };

            var response = await Retriable(async () =>
                await DefaultApi.FinancialClient().Query(query), HasFinancialActions);
            ;

            response.ShouldNotBeNull();
            if (!response.Data.IsNullOrEmpty())
            {
                foreach (var action in response.Data)
                {
                    action.PaymentId.ShouldNotBeNull();
                    action.ActionId.ShouldNotBeNull();
                    action.ActionType.ShouldNotBeNull();
                    action.EntityId.ShouldNotBeNull();
                    action.CurrencyAccountId.ShouldNotBeNull();
                    action.ProcessedOn.ShouldNotBeNull();
                    action.RequestedOn.ShouldNotBeNull();
                    action.PaymentId.ShouldBe(payment.Id);
                }
            }
        }
        
        private static bool HasFinancialActions(FinancialActionsQueryResponse obj)
        {
            return obj.Count > 0;
        }
    }
}

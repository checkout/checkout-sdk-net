using Checkout.Common;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments.Sessions
{

    public class PaymentSessionsIntegrationTest : SandboxTestFixture
    {
        public PaymentSessionsIntegrationTest() : base(PlatformType.Default)
        {
        }

        [Fact]
        private async Task ShouldMakeAPaymentSessionsRequest()
        {
            var billing = new Billing
            {
                Address = GetAddress()
            };
            
            var paymentSessionsRequest = new PaymentSessionsRequest
            {
                Amount = 2000,
                Currency = Currency.GBP,
                Reference = "ORD-123A",
                Billing = billing,
                Customer = GetCustomer(),
                SuccessUrl = "https://example.com/payments/success",
                FailureUrl = "https://example.com/payments/fail",
            };

            var response = await DefaultApi.PaymentSessionsClient().RequestPaymentSessions(paymentSessionsRequest);

            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.Locale.ShouldBe("en-GB");
            response.Currency.ShouldBe(Currency.GBP);
            response.PaymentMethods.ShouldNotBeNull();
            response.Links.ShouldNotBeNull();
        }
    }
}
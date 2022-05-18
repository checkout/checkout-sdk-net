using Checkout.Common;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments.Hosted
{
    public class HostedPaymentsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact]
        private async Task ShouldCreateAndGetHostedPayment()
        {
            var hostedPaymentRequest = CreateHostedPaymentRequest("ORD-123A");

            var createResponse = await DefaultApi.HostedPaymentsClient().CreateHostedPaymentsPageSession(hostedPaymentRequest);

            createResponse.ShouldNotBeNull();
            createResponse.Id.ShouldNotBeNullOrEmpty();
            createResponse.Reference.ShouldNotBeNullOrEmpty();
            createResponse.Links.ShouldNotBeNull();
            createResponse.Links.ContainsKey("redirect").ShouldBeTrue();

            var getResponse = await DefaultApi.HostedPaymentsClient().GetHostedPaymentsPageDetails(createResponse.Id);

            getResponse.ShouldNotBeNull();
            getResponse.Id.ShouldNotBeNullOrEmpty();
            getResponse.Reference.ShouldNotBeNullOrEmpty();
            getResponse.Status.ShouldBe(HostedPaymentStatus.PaymentPending);
            getResponse.Amount.ShouldNotBeNull();
            getResponse.Billing.ShouldNotBeNull();
            getResponse.Currency.ShouldBe(Currency.GBP);
            getResponse.Customer.ShouldNotBeNull();
            getResponse.Description.ShouldNotBeNullOrEmpty();
            getResponse.FailureUrl.ShouldNotBeNull();
            getResponse.SuccessUrl.ShouldNotBeNull();
            getResponse.CancelUrl.ShouldNotBeNullOrEmpty();
            getResponse.Links.Count.ShouldBe(2);
            getResponse.Products.Count.ShouldBe(1);
        }
    }
}
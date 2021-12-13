using Checkout.Common;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments.Hosted
{
    public class HostedPaymentsTestIT : AbstractPaymentsIntegrationTest
    {
        private const string Reference = "ORD-123A";

        [Fact]
        private async Task ShouldCreateHostedPayments()
        {
            var request = CreateHostedPaymentRequest(Reference);
            var response = await DefaultApi.HostedPaymentsClient().Create(request);

            response.ShouldNotBeNull();
            response.Reference.ShouldBe(Reference);
            response.Links.ShouldNotBeNull();
            response.Links.ContainsKey("redirect").ShouldBeTrue();
        }
    }
}

using Checkout.Common;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Forex
{
    public class ForexIntegrationTest : SandboxTestFixture
    {
        public ForexIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact]
        private async Task ShouldRequestQuote()
        {
            var request = new QuoteRequest()
            {
                SourceCurrency = Currency.GBP,
                SourceAmount = 30000L,
                DestinationCurrency = Currency.USD,
                ProcessChannelId = "pc_abcdefghijklmnopqrstuvwxyz"
            };
            var response = await DefaultApi.ForexClient().RequestQuote(request);

            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.SourceCurrency.ShouldBe(request.SourceCurrency);
            response.SourceAmount.ShouldBe(request.SourceAmount);
            response.DestinationCurrency.ShouldBe(request.DestinationCurrency);
            response.DestinationAmount.ShouldNotBeNull();
            response.Rate.ShouldNotBeNull();
            response.ExpiresOn.ShouldNotBeNull();
            response.IsSingleUse.ShouldNotBeNull();
        }
    }
}
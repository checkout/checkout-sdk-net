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
        
        [Fact(Skip = "Skipping because processing_channel_id is invalid")]
        private async Task ShouldGetRates()
        {
            var query = new RatesQueryFilter()
            {
                Product = "card_payouts",
                Source = ForexSource.Visa,
                CurrencyPairs = "GBPEUR,USDNOK,JPNCAD",
                ProcessChannelId = "pc_vxt6yftthv4e5flqak6w2i7rim"
            };
            var response = await DefaultApi.ForexClient().GetRates(query);

            response.ShouldNotBeNull();
            response.Product.ShouldNotBeNull();
            response.Product.ShouldBe(query.Product);
            response.Source.ShouldBe(query.Source);
            response.Rates.ShouldNotBeNull();
        }
    }
}
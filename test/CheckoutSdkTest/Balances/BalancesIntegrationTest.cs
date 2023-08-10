using Checkout.Common;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Balances
{
    public class BalancesIntegrationTest : SandboxTestFixture
    {
        public BalancesIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }
        
        [Fact]
        private async Task ShouldRetrieveEntityBalancesWithOutQuery()
        {
            var query = new BalancesQuery {Query = null};

            var balances = await DefaultApi.BalancesClient()
                .RetrieveEntityBalances("ent_kidtcgc3ge5unf4a5i6enhnr5m", query);
            balances.ShouldNotBeNull();
            balances.Data.ShouldNotBeNull();
            foreach (var balance in balances.Data)
            {
                balance.CurrencyAccountId.ShouldBeNull();
                balance.Descriptor.ShouldNotBeNull();
                balance.HoldingCurrency.ShouldNotBeNull();
                balance.Balances.ShouldNotBeNull();
            }
        }

        [Fact]
        private async Task ShouldRetrieveEntityBalances()
        {
            var query = new BalancesQuery {Query = "currency:" + Currency.GBP};

            var balances = await DefaultApi.BalancesClient()
                .RetrieveEntityBalances("ent_kidtcgc3ge5unf4a5i6enhnr5m", query);
            balances.ShouldNotBeNull();
            balances.Data.ShouldNotBeNull();
            foreach (var balance in balances.Data)
            {
                balance.CurrencyAccountId.ShouldBeNull();
                balance.Descriptor.ShouldNotBeNull();
                balance.HoldingCurrency.ShouldNotBeNull();
                balance.Balances.ShouldNotBeNull();
            }
        }
        
        [Fact]
        private async Task ShouldRetrieveEntityBalancesWithCurrencyAccountId()
        {
            var query = new BalancesQuery
            {
                Query = "currency:" + Currency.GBP,
                WithCurrencyAccountId = true,
            };

            var balances = await DefaultApi.BalancesClient()
                .RetrieveEntityBalances("ent_kidtcgc3ge5unf4a5i6enhnr5m", query);
            balances.ShouldNotBeNull();
            balances.Data.ShouldNotBeNull();
            foreach (var balance in balances.Data)
            {
                balance.CurrencyAccountId.ShouldNotBeNull();
                balance.Descriptor.ShouldNotBeNull();
                balance.HoldingCurrency.ShouldNotBeNull();
                balance.Balances.ShouldNotBeNull();
            }
        }
    }
}
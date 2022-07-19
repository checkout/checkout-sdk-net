using Checkout.Common;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Balances
{
    public class BalancesIntegrationTest : SandboxTestFixture
    {
        public BalancesIntegrationTest() : base(PlatformType.FourOAuth)
        {
        }

        [Fact]
        private async Task ShouldRetrieveEntityBalances()
        {
            var query = new BalancesQuery {Query = "currency:" + Currency.GBP};

            var balances = await FourApi.BalancesClient()
                .RetrieveEntityBalances("ent_kidtcgc3ge5unf4a5i6enhnr5m", query);
            balances.ShouldNotBeNull();
            balances.Data.ShouldNotBeNull();
            foreach (var balance in balances.Data)
            {
                balance.Descriptor.ShouldNotBeNull();
                balance.HoldingCurrency.ShouldNotBeNull();
                balance.Balances.ShouldNotBeNull();
            }
        }
    }
}
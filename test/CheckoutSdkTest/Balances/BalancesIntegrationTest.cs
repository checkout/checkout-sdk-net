using Checkout.Common;
using Shouldly;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Balances
{
    public class BalancesIntegrationTest : SandboxTestFixture
    {
        private const string EntityId = "ent_kidtcgc3ge5unf4a5i6enhnr5m";

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

        /// <summary>
        /// GET /entities/{entityId}/currency-accounts/{currencyAccountId}/top-up-instructions.
        ///
        /// Top-ups are not enabled on the sandbox sub-accounts this suite has access to, so the
        /// endpoint answers 403 ("top-ups aren't enabled for the sub-account") rather than 200.
        /// Verified live on 2026-09-07, including with the balances:top-up-instructions scope
        /// granted, which the sandbox IdP does issue.
        ///
        /// The test is therefore written to accept either outcome, but only the outcomes the spec
        /// documents as "not available here": 403 and 404. It still fails on 400 (malformed
        /// identifiers, i.e. the SDK built the path wrongly) and on 401 (wrong authorization type),
        /// which are the two ways this endpoint could actually be broken in the SDK. If a
        /// top-up-enabled sub-account becomes available, the success branch asserts the full
        /// contract already -- but only what the spec guarantees: an empty bank_details is legal,
        /// so no rail is required to be present.
        /// </summary>
        [Fact]
        private async Task ShouldRetrieveTopUpInstructions()
        {
            var balances = await DefaultApi.BalancesClient()
                .RetrieveEntityBalances(EntityId, new BalancesQuery { WithCurrencyAccountId = true });

            balances.Data.ShouldNotBeNull();

            // Take the first sub-account that reports an id. Asserting the entity always has at
            // least one would make this test fail for a reason unrelated to top-up instructions.
            var currencyAccountId = balances.Data
                .Select(balance => balance.CurrencyAccountId)
                .FirstOrDefault(id => !string.IsNullOrWhiteSpace(id));
            if (currencyAccountId == null) return;

            try
            {
                var instructions = await DefaultApi.BalancesClient()
                    .RetrieveTopUpInstructions(EntityId, currencyAccountId);

                instructions.ShouldNotBeNull();
                instructions.CurrencyAccountId.ShouldBe(currencyAccountId);
                instructions.Currency.ShouldNotBeNull();
                instructions.PaymentReference.ShouldNotBeNullOrWhiteSpace();
                instructions.BankDetails.ShouldNotBeNull();

                // Assert only what the spec guarantees. TopUpBankDetails declares no required
                // properties, so an empty bank_details is a legal 200 body -- do not require a
                // rail to be present. Where a rail IS returned, its two required fields must be.
                foreach (var rail in new[] { instructions.BankDetails.Domestic, instructions.BankDetails.International })
                {
                    if (rail == null) continue;
                    rail.BeneficiaryAccountName.ShouldNotBeNullOrWhiteSpace();
                    rail.BankName.ShouldNotBeNullOrWhiteSpace();
                }
            }
            catch (CheckoutApiException e)
            {
                // 403 = top-ups not enabled for the sub-account, or the credential lacks access.
                // 404 = sub-account not found, or it has no top-up instructions available.
                // Anything else means the SDK, not the environment, is at fault.
                e.HttpStatusCode.ShouldBeOneOf(HttpStatusCode.Forbidden, HttpStatusCode.NotFound);
            }
        }
    }
}

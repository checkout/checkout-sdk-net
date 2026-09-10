using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Balances
{
    public class BalancesClient : AbstractClient, IBalancesClient
    {
        private const string BalancesPath = "balances";
        private const string EntitiesPath = "entities";
        private const string CurrencyAccountsPath = "currency-accounts";
        private const string TopUpInstructionsPath = "top-up-instructions";

        public BalancesClient(IApiClient apiClient,
            CheckoutConfiguration configuration)
            : base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        public async Task<BalancesResponse> RetrieveEntityBalances(string entityId, BalancesQuery balancesQuery,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId, "balancesQuery", balancesQuery);
            return await ApiClient.Query<BalancesResponse>(BuildPath(BalancesPath, entityId),
                SdkAuthorization(),
                balancesQuery,
                cancellationToken);
        }

        public async Task<TopUpInstructionsResponse> RetrieveTopUpInstructions(string entityId,
            string currencyAccountId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId, "currencyAccountId", currencyAccountId);
            return await ApiClient.Get<TopUpInstructionsResponse>(
                BuildPath(EntitiesPath, entityId, CurrencyAccountsPath, currencyAccountId, TopUpInstructionsPath),
                SdkAuthorization(),
                cancellationToken);
        }
    }
}

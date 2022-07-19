using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Balances
{
    public class BalancesClient : AbstractClient, IBalancesClient
    {
        private const string BalancesPath = "balances";

        public BalancesClient(IApiClient apiClient,
            CheckoutConfiguration configuration)
            : base(apiClient, configuration, SdkAuthorizationType.OAuth)
        {
        }

        public async Task<BalancesResponse> RetrieveEntityBalances(string entityId, BalancesQuery balancesQuery,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("filter", entityId, "balancesQuery", balancesQuery);
            return await ApiClient.Query<BalancesResponse>(BuildPath(BalancesPath, entityId),
                SdkAuthorization(),
                balancesQuery,
                cancellationToken);
        }
    }
}
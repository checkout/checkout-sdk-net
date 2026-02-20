using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Financial
{
    public class FinancialClient : AbstractClient, IFinancialClient
    {
        private const string FinancialActionsPath = "financial-actions";

        public FinancialClient(IApiClient apiClient,
            CheckoutConfiguration configuration) : base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        public Task<FinancialActionsQueryResponse> Query(
            FinancialActionsQueryFilter query,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("query", query);
            return ApiClient.Query<FinancialActionsQueryResponse>(
                FinancialActionsPath,
                SdkAuthorization(),
                query,
                cancellationToken);
        }
    }
}

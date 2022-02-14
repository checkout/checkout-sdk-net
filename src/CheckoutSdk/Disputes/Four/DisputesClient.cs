using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Disputes.Four
{
    public class DisputesClient : Disputes.DisputesClient, IDisputesClient
    {
        private const string DisputesPath = "disputes";

        public DisputesClient(IApiClient apiClient,
            CheckoutConfiguration configuration) : base(apiClient, configuration)
        {
        }

        public Task<DisputesQueryResponse> Query(DisputesQueryFilter filter,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("filter", filter);
            return ApiClient.Query<DisputesQueryResponse>(DisputesPath, SdkAuthorization(), filter,
                cancellationToken);
        }

        public new Task<DisputeDetailsResponse> GetDisputeDetails(string disputeId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("disputeId", disputeId);
            return ApiClient.Get<DisputeDetailsResponse>(BuildPath(DisputesPath, disputeId), SdkAuthorization(),
                cancellationToken);
        }
    }
}
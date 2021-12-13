using Checkout.Files;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Disputes
{
    public class DisputesClient : FilesClient, IDisputesClient
    {
        private const string DisputesPath = "disputes";
        private const string EvidencePath = "evidence";
        private const string AcceptPath = "accept";

        public DisputesClient(IApiClient apiClient,
            CheckoutConfiguration configuration) : base(apiClient, configuration)
        {
        }

        public Task<DisputesQueryResponse> Query(DisputesQueryFilter filter,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("filter", filter);
            return ApiClient.Query<DisputesQueryResponse>(DisputesPath, SdkAuthorization(), filter, cancellationToken);
        }

        public Task<DisputeDetailsResponse> GetDisputeDetails(string disputeId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("disputeId", disputeId);
            return ApiClient.Get<DisputeDetailsResponse>(BuildPath(DisputesPath, disputeId), SdkAuthorization(),
                cancellationToken);
        }

        public Task<object> Accept(string disputeId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("disputeId", disputeId);
            return ApiClient.Post<object>(BuildPath(DisputesPath, disputeId, AcceptPath), SdkAuthorization(), null,
                cancellationToken);
        }

        public Task<object> PutEvidence(string disputeId, DisputeEvidenceRequest disputeEvidenceRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("disputeId", disputeId, "disputeEvidenceRequest", disputeEvidenceRequest);
            return ApiClient.Put<object>(BuildPath(DisputesPath, disputeId, EvidencePath), SdkAuthorization(),
                disputeEvidenceRequest,
                cancellationToken);
        }

        public Task<DisputeEvidenceResponse> GetEvidence(string disputeId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("disputeId", disputeId);
            return ApiClient.Get<DisputeEvidenceResponse>(BuildPath(DisputesPath, disputeId, EvidencePath),
                SdkAuthorization(),
                cancellationToken);
        }

        public Task<object> SubmitEvidence(string disputeId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("disputeId", disputeId);
            return ApiClient.Post<object>(BuildPath(DisputesPath, disputeId, EvidencePath), SdkAuthorization(), null,
                cancellationToken);
        }
    }
}
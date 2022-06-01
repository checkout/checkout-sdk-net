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
            CheckoutConfiguration configuration) : base(apiClient, null, configuration)
        {
        }

        public Task<DisputesQueryResponse> Query(DisputesQueryFilter filter,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("filter", filter);
            return ApiClient.Query<DisputesQueryResponse>(DisputesPath, SdkAuthorization(), filter,
                cancellationToken);
        }

        public Task<DisputeDetailsResponse> GetDisputeDetails(string disputeId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("disputeId", disputeId);
            return ApiClient.Get<DisputeDetailsResponse>(BuildPath(DisputesPath, disputeId), SdkAuthorization(),
                cancellationToken);
        }

        public Task<EmptyResponse> Accept(string disputeId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("disputeId", disputeId);
            return ApiClient.Post<EmptyResponse>(BuildPath(DisputesPath, disputeId, AcceptPath), SdkAuthorization(), null,
                cancellationToken, null);
        }

        public Task<EmptyResponse> PutEvidence(string disputeId, DisputeEvidenceRequest disputeEvidenceRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("disputeId", disputeId, "disputeEvidenceRequest", disputeEvidenceRequest);
            return ApiClient.Put<EmptyResponse>(BuildPath(DisputesPath, disputeId, EvidencePath), SdkAuthorization(),
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

        public Task<EmptyResponse> SubmitEvidence(string disputeId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("disputeId", disputeId);
            return ApiClient.Post<EmptyResponse>(BuildPath(DisputesPath, disputeId, EvidencePath), SdkAuthorization(), null,
                cancellationToken, null);
        }
    }
}
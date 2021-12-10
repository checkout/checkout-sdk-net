using Checkout.Files;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Disputes.Four
{
    public interface IDisputesClient : IFilesClient
    {
        Task<DisputesQueryResponse> Query(DisputesQueryFilter filter, CancellationToken cancellationToken = default);

        Task<DisputeDetailsResponse> GetDisputeDetails(string disputeId, CancellationToken cancellationToken = default);

        Task<object> Accept(string id, CancellationToken cancellationToken = default);

        Task<object> PutEvidence(string id, DisputeEvidenceRequest disputeEvidenceRequest,
            CancellationToken cancellationToken = default);

        Task<DisputeEvidenceResponse> GetEvidence(string id, CancellationToken cancellationToken = default);

        Task<object> SubmitEvidence(string id, CancellationToken cancellationToken = default);
    }
}
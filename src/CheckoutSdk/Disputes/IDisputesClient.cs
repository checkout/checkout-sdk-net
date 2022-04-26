using Checkout.Common;
using Checkout.Files;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Disputes
{
    public interface IDisputesClient : IFilesClient
    {
        Task<DisputesQueryResponse> Query(DisputesQueryFilter filter, CancellationToken cancellationToken = default);

        Task<DisputeDetailsResponse> GetDisputeDetails(string disputeId, CancellationToken cancellationToken = default);

        Task<object> Accept(string disputeId, CancellationToken cancellationToken = default);

        Task<object> PutEvidence(string disputeId, DisputeEvidenceRequest disputeEvidenceRequest,
            CancellationToken cancellationToken = default);

        Task<DisputeEvidenceResponse> GetEvidence(string disputeId, CancellationToken cancellationToken = default);

        Task<object> SubmitEvidence(string disputeId, CancellationToken cancellationToken = default);
    }
}
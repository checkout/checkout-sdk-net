using Checkout.Files;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Disputes
{
    public interface IDisputesClient : IFilesClient
    {
        Task<DisputesQueryResponse> Query(DisputesQueryFilter filter, CancellationToken cancellationToken = default);

        Task<DisputeDetailsResponse> GetDisputeDetails(string disputeId, CancellationToken cancellationToken = default);

        Task<EmptyResponse> Accept(string disputeId, CancellationToken cancellationToken = default);

        Task<EmptyResponse> PutEvidence(string disputeId, DisputeEvidenceRequest disputeEvidenceRequest,
            CancellationToken cancellationToken = default);

        Task<DisputeEvidenceResponse> GetEvidence(string disputeId, CancellationToken cancellationToken = default);

        Task<EmptyResponse> SubmitEvidence(string disputeId, CancellationToken cancellationToken = default);
        
        Task<EmptyResponse> SubmitArbitrationEvidence(string disputeId, CancellationToken cancellationToken = default);

        Task<DisputeCompiledSubmittedEvidenceResponse> GetCompiledSubmittedEvidence(string disputeId,
            CancellationToken cancellationToken = default);
        
        Task<DisputeCompiledSubmittedEvidenceResponse> GetCompiledSubmittedArbitrationEvidence(string disputeId,
            CancellationToken cancellationToken = default);

        Task<SchemeFileResponse> GetDisputeSchemeFiles(string disputeId, CancellationToken cancellationToken = default);
    }
}
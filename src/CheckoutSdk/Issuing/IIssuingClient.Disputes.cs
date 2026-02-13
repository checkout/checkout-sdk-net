using Checkout.Issuing.Disputes.Requests;
using Checkout.Issuing.Disputes.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Issuing
{
    public partial interface IIssuingClient
    {
        Task<IssuingDisputeResponse> CreateDispute(
            CreateDisputeRequest createDisputeRequest,
            CancellationToken cancellationToken = default);

        Task<IssuingDisputeResponse> GetDisputeDetails(
            string disputeId,
            CancellationToken cancellationToken = default);

        Task<EmptyResponse> CancelDispute(
            string disputeId,
            CancellationToken cancellationToken = default);

        Task<EmptyResponse> EscalateDispute(
            string disputeId,
            EscalateDisputeRequest escalateDisputeRequest,
            CancellationToken cancellationToken = default);

        Task<IssuingDisputeResponse> SubmitDispute(
            string disputeId,
            SubmitDisputeRequest submitDisputeRequest = null,
            CancellationToken cancellationToken = default);
    }
}
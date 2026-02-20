using Checkout.Issuing.Disputes.Requests;
using Checkout.Issuing.Disputes.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Issuing
{
    public partial interface IIssuingClient
    {
        /// <summary>
        /// Create a new Issuing dispute for a transaction.
        /// [Beta]
        /// </summary>
        /// <param name="createDisputeRequest">The dispute creation request details.</param>
        /// <param name="idempotencyKey">A unique idempotency key for safely retrying requests.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>The created dispute details.</returns>
        Task<IssuingDisputeResponse> CreateDispute(
            CreateDisputeRequest createDisputeRequest,
            string idempotencyKey,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get the details of an existing Issuing dispute.
        /// [Beta]
        /// </summary>
        /// <param name="disputeId">The unique identifier of the dispute.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>The dispute details.</returns>
        Task<IssuingDisputeResponse> GetDisputeDetails(
            string disputeId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Cancel an existing Issuing dispute.
        /// [Beta]
        /// </summary>
        /// <param name="disputeId">The unique identifier of the dispute to cancel.</param>
        /// <param name="idempotencyKey">A unique idempotency key for safely retrying requests.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>An empty response indicating success.</returns>
        Task<EmptyResponse> CancelDispute(
            string disputeId,
            string idempotencyKey,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Escalate an Issuing dispute to the next level (chargeback, pre-arbitration, or arbitration).
        /// [Beta]
        /// </summary>
        /// <param name="disputeId">The unique identifier of the dispute to escalate.</param>
        /// <param name="idempotencyKey">A unique idempotency key for safely retrying requests.</param>
        /// <param name="escalateDisputeRequest">The escalation request details.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>An empty response indicating success.</returns>
        Task<EmptyResponse> EscalateDispute(
            string disputeId,            
            string idempotencyKey,
            EscalateDisputeRequest escalateDisputeRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Submit updates to an existing Issuing dispute.
        /// [Beta]
        /// </summary>
        /// <param name="disputeId">The unique identifier of the dispute to update.</param>
        /// <param name="idempotencyKey">A unique idempotency key for safely retrying requests.</param>
        /// <param name="submitDisputeRequest">The optional update request details.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>The updated dispute details.</returns>
        Task<IssuingDisputeResponse> SubmitDispute(
            string disputeId,
            string idempotencyKey,
            SubmitDisputeRequest submitDisputeRequest = null,
            CancellationToken cancellationToken = default);
    }
}
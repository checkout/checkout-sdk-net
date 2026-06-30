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
        /// Submit an Issuing dispute to the card scheme for processing.
        /// [Beta]
        /// </summary>
        /// <remarks>
        /// This endpoint is deprecated. Use <see cref="CreateDispute"/> to create and submit a dispute in a
        /// single step, or <see cref="AmendDispute"/> if the dispute status is action_required.
        /// </remarks>
        /// <param name="disputeId">The unique identifier of the dispute to submit.</param>
        /// <param name="idempotencyKey">A unique idempotency key for safely retrying requests.</param>
        /// <param name="submitDisputeRequest">The optional submit request details.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>The dispute details.</returns>
        [System.Obsolete("POST /issuing/disputes/{disputeId}/submit is deprecated. Use CreateDispute to create and submit a dispute in a single step, or AmendDispute if the dispute status is action_required.", false)]
        Task<IssuingDisputeResponse> SubmitDispute(
            string disputeId,
            string idempotencyKey,
            SubmitDisputeRequest submitDisputeRequest = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Amend an Issuing dispute that is currently blocked from proceeding. Handles both
        /// chargeback-stage and escalation-stage amendments.
        /// [Beta]
        /// </summary>
        /// <param name="disputeId">The unique identifier of the dispute to amend.</param>
        /// <param name="idempotencyKey">A unique idempotency key for safely retrying requests.</param>
        /// <param name="amendDisputeRequest">The optional amend request details.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>The dispute details.</returns>
        Task<IssuingDisputeResponse> AmendDispute(
            string disputeId,
            string idempotencyKey,
            AmendDisputeRequest amendDisputeRequest = null,
            CancellationToken cancellationToken = default);
    }
}
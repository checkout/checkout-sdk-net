using Checkout.Common;
using System;

namespace Checkout.Issuing.Disputes.Responses
{
    /// <summary>
    /// Response containing Issuing dispute information.
    /// [Beta]
    /// </summary>
    public class IssuingDisputeResponse : Resource
    {
        /// <summary>
        /// The unique identifier for the Issuing dispute.
        /// ^idsp_[a-z0-9]{26}$
        /// 31 characters
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The four-digit scheme-specific reason code you provide for the chargeback.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// The disputed amount, in the minor unit of the transaction currency.
        /// </summary>
        public DisputeAmount DisputedAmount { get; set; }

        /// <summary>
        /// The dispute status.
        /// Enum: "created", "canceled", "processing", "action_required", "won", "lost"
        /// </summary>
        public IssuingDisputeStatus? Status { get; set; }

        /// <summary>
        /// The status reason, which provides more information about the dispute status.
        /// Enum: "expired", "chargeback_pending", "chargeback_evidence_invalid_or_insufficient", "chargeback_processed", "chargeback_rejected", "chargeback_reversal_pending", "chargeback_reversed", "chargeback_response_accepted", "prearbitration_pending", "prearbitration_evidence_invalid_or_insufficient", "prearbitration_processed", "prearbitration_rejected", "prearbitration_reversal_pending", "prearbitration_reversed", "prearbitration_response_accepted", "arbitration_pending", "arbitration_processed", "presentment_reversed"
        /// </summary>
        public IssuingDisputeStatusReason? StatusReason { get; set; }

        /// <summary>
        /// The unique Checkout.com identifier for the transaction.
        /// ^trx_[a-z0-9]{26}$
        /// 30 characters
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// The unique identifier for the disputed presentment message.
        /// ^msg_[a-z0-9]{26}$
        /// 30 characters
        /// </summary>
        public string PresentmentMessageId { get; set; }

        /// <summary>
        /// The details of the merchant you raised the dispute with.
        /// </summary>
        public DisputeMerchant Merchant { get; set; }

        /// <summary>
        /// The date and time when the dispute was created, in UTC.
        /// Format – ISO 8601 code
        /// Example – 2025-01-31T10:20:30.456
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// The date and time when the dispute was last modified, in UTC.
        /// Format – ISO 8601 code
        /// Example – 2025-01-31T10:20:30.456
        /// </summary>
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        /// The dispute details at the chargeback stage.
        /// </summary>
        public DisputeChargeback Chargeback { get; set; }

        /// <summary>
        /// The information provided by the merchant when they reject the chargeback and send a representment.
        /// </summary>
        public DisputeRepresentment Representment { get; set; }

        /// <summary>
        /// The dispute details at the pre-arbitration stage.
        /// </summary>
        public DisputePreArbitration PreArbitration { get; set; }

        /// <summary>
        /// The dispute details during the arbitration stage.
        /// </summary>
        public DisputeArbitration Arbitration { get; set; }
    }
}
using Checkout.Common;
using System;

namespace Checkout.Disputes
{
    public class DisputeSummary : Resource
    {
        /// <summary>
        /// The dispute identifier.
        /// [Optional]
        /// ^(dsp)_(\w{22,26})$
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The reason for the dispute.
        /// [Optional]
        /// </summary>
        public DisputeCategory? Category { get; set; }

        /// <summary>
        /// The current status of the dispute.
        /// [Optional]
        /// </summary>
        public DisputeStatus? Status { get; set; }

        /// <summary>
        /// The disputed amount in the processing currency.
        /// [Optional]
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// The processing currency.
        /// [Optional]
        /// </summary>
        public Currency? Currency { get; set; }

        /// <summary>
        /// The card scheme reason code for the dispute.
        /// [Optional]
        /// </summary>
        public string ReasonCode { get; set; }

        /// <summary>
        /// The payment identifier.
        /// [Optional]
        /// </summary>
        public string PaymentId { get; set; }

        /// <summary>
        /// The payment action identifier.
        /// [Optional]
        /// </summary>
        public string PaymentActionId { get; set; }

        /// <summary>
        /// The payment reference or order ID.
        /// [Optional]
        /// </summary>
        public string PaymentReference { get; set; }

        /// <summary>
        /// The acquirer reference number for the payment.
        /// [Optional]
        /// </summary>
        public string PaymentArn { get; set; }

        /// <summary>
        /// The payment method or card scheme.
        /// [Optional]
        /// </summary>
        public string PaymentMethod { get; set; }

        /// <summary>
        /// The deadline by which evidence must be submitted.
        /// [Optional]
        /// Format: date-time (RFC 3339)
        /// </summary>
        public DateTime? EvidenceRequiredBy { get; set; }

        /// <summary>
        /// The date and time the dispute was issued.
        /// [Optional]
        /// Format: date-time (RFC 3339)
        /// </summary>
        public DateTime? ReceivedOn { get; set; }

        /// <summary>
        /// The date and time of the last update to the dispute.
        /// [Optional]
        /// Format: date-time (RFC 3339)
        /// </summary>
        public DateTime? LastUpdate { get; set; }

        /// <summary>
        /// The reason the dispute was automatically resolved.
        /// [Optional]
        /// Enum: "rapid_dispute_resolution" "negative_amount" "already_refunded"
        /// </summary>
        public DisputeResolvedReason? ResolvedReason { get; set; }

        /// <summary>
        /// Whether the dispute is eligible for Visa Compelling Evidence 3.0.
        /// [Optional]
        /// </summary>
        public bool? IsCeCandidate { get; set; }

        // Not available on Previous API

        /// <summary>
        /// The client entity linked to this dispute.
        /// [Optional]
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// The sub-entity linked to this dispute.
        /// [Optional]
        /// </summary>
        public string SubEntityId { get; set; }

        /// <summary>
        /// The processing channel linked to this dispute.
        /// [Optional]
        /// </summary>
        public string ProcessingChannel { get; set; }

        /// <summary>
        /// The business segment identifier.
        /// [Optional]
        /// </summary>
        public string SegmentId { get; set; }

        /// <summary>
        /// The merchant category code for the payment.
        /// [Optional]
        /// </summary>
        public string PaymentMcc { get; set; }

    }
}

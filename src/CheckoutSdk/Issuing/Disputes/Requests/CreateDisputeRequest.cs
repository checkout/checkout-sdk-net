using System.Collections.Generic;
using Checkout.Issuing.Disputes.Common;

namespace Checkout.Issuing.Disputes.Requests
{
    /// <summary>
    /// Create a dispute for an Issuing transaction. For full guidance, see Manage Issuing disputes.
    /// The transaction must already be cleared and not refunded.
    /// [Beta]
    /// </summary>
    public class CreateDisputeRequest
    {
        /// <summary>
        /// The transaction's unique identifier.
        /// [Required]
        /// ^trx_[a-z0-9]{26}$
        /// 30 characters
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// The four-digit scheme-specific reason code for the chargeback.
        /// Only provide this if Checkout.com is your issuing processor.
        /// Checkout.com does not validate this value.
        /// [Required]
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Your evidence for raising the chargeback, in line with the card scheme's requirements.
        /// [Optional]
        /// </summary>
        public IList<DisputeEvidence> Evidence { get; set; }

        /// <summary>
        /// The chargeback amount, in the minor unit of the transaction currency.
        /// If not provided, Checkout.com uses the full amount of the presentment.
        /// [Optional]
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// The unique identifier for the disputed presentment message, if the transaction has multiple presentments.
        /// If the transaction has only one presentment, Checkout.com uses this automatically.
        /// [Optional]
        /// ^msg_[a-z0-9]{26}$
        /// 30 characters
        /// </summary>
        public string PresentmentMessageId { get; set; }

        /// <summary>
        /// Indicates whether to submit the dispute immediately (true) or later (false).
        /// </summary>
        /// <remarks>
        /// This property is deprecated. Use CreateDispute directly to create and submit in a single step,
        /// or AmendDispute if the dispute status is action_required.
        /// </remarks>
        [System.Obsolete("This property is deprecated. Use CreateDispute to create and submit a dispute in a single step, or AmendDispute if the dispute status is action_required.", false)]
        public bool? IsReadyForSubmission { get; set; }

        /// <summary>
        /// Your justification for the chargeback.
        /// [Optional]
        /// &lt;= 100 characters
        /// </summary>
        public string Justification { get; set; }

        /// <summary>
        /// Contains all fraud-related information to be sent with the chargeback.
        /// This field is required if the dispute has a fraud-related reason code.
        /// [Optional]
        /// </summary>
        public IssuingDisputeFraudDetails FraudDetails { get; set; }
    }
}
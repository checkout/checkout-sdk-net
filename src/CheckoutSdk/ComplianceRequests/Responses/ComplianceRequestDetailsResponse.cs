using Checkout.Common;

namespace Checkout.ComplianceRequests.Responses
{
    /// <summary>
    /// Represents a compliance request retrieved by payment identifier.
    /// </summary>
    public class ComplianceRequestDetailsResponse : Resource
    {
        /// <summary>
        /// The compliance request's payment ID.
        /// [Optional]
        /// Nullable.
        /// </summary>
        public string PaymentId { get; set; }

        /// <summary>
        /// The compliance request's client ID.
        /// [Optional]
        /// Nullable.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// The compliance request's entity ID.
        /// [Optional]
        /// Nullable.
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// The compliance request's segment ID.
        /// [Optional]
        /// Nullable.
        /// </summary>
        public string SegmentId { get; set; }

        /// <summary>
        /// The payment's transaction amount.
        /// [Optional]
        /// Nullable.
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// The payment's transaction currency, as a three-letter ISO currency code.
        /// [Optional]
        /// Format: three-letter ISO currency code.
        /// Nullable.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// The payment's recipient's name.
        /// [Optional]
        /// Nullable.
        /// </summary>
        public string RecipientName { get; set; }

        /// <summary>
        /// The date and time when the compliance request was initiated.
        /// [Optional]
        /// Nullable.
        /// </summary>
        public string RequestedOn { get; set; }

        /// <summary>
        /// The compliance request's status.
        /// [Optional]
        /// Nullable.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The fields requested for this compliance request.
        /// [Optional]
        /// Nullable.
        /// </summary>
        public ComplianceRequestedFields Fields { get; set; }

        /// <summary>
        /// The payment's transaction reference.
        /// [Optional]
        /// Nullable.
        /// </summary>
        public string TransactionReference { get; set; }

        /// <summary>
        /// The payment sender's reference.
        /// [Optional]
        /// Nullable.
        /// </summary>
        public string SenderReference { get; set; }

        /// <summary>
        /// The date and time when the compliance request was last updated.
        /// [Optional]
        /// Nullable.
        /// </summary>
        public string LastUpdated { get; set; }

        /// <summary>
        /// The payment sender's name.
        /// [Optional]
        /// Nullable.
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// The payment type.
        /// [Optional]
        /// Nullable.
        /// </summary>
        public string PaymentType { get; set; }
    }
}

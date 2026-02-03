using Checkout.Common;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseAccepted
{
    /// <summary>
    /// Request a payment or payout Response 202
    /// Payment asynchronous or further action required
    /// </summary>
    public class RequestAPaymentOrPayoutResponseAccepted : Resource
    {
        /// <summary>
        /// The payment's unique identifier.
        /// = 30 characters ^(pay)_(\w{26})$
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The refund status.
        /// Enum: "Accepted" "Rejected" "Pending"
        /// </summary>
        public StatusType Status { get; set; }

        /// <summary>
        /// The payment's unique identifier.
        /// The reference you provided in the refund request.
        /// &lt;= 50 characters
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Instruction details for payouts to bank accounts.
        /// </summary>
        public Instruction.Instruction Instruction { get; set; }

        /// <summary>
        /// The refund destination.
        /// </summary>
        public Destination.Destination Destination { get; set; }
    }
}
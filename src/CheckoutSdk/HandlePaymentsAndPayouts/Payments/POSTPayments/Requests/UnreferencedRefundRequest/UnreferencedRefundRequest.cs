using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination;
using System.Collections.Generic;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest
{
    /// <summary>
    /// Request a payment or payout
    /// Send a payment or payout.Note: successful payout requests will always return a 202 response.
    /// </summary>
    public class UnreferencedRefundRequest
    {
        /// <summary>
        /// The source of the unreferenced refund.
        /// [Required]
        /// </summary>
        public Source.Source Source { get; set; }

        /// <summary>
        /// The destination of the unreferenced refund.
        /// [Required]
        /// </summary>
        public AbstractDestination Destination { get; set; }

        /// <summary>
        /// The amount of the payment
        /// [Required]
        /// >= 1
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// The three-letter ISO currency code of the payment.
        /// [Required]
        /// 3 characters
        /// </summary>
        public Currency Currency { get; set; }

        /// <summary>
        /// The type of the payment
        /// [Required]
        /// </summary>
        public string PaymentType { get; set; }

        /// <summary>
        /// The processing channel identifier
        /// [Required]
        /// ^(pc)_(\w{26})$
        /// </summary>
        public string ProcessingChannelId { get; set; }

        /// <summary>
        /// Additional details about the unreferenced refund instruction.
        /// [Optional]
        /// </summary>
        public Instruction.Instruction Instruction { get; set; }

        /// <summary>
        /// An internal reference you can later use to identify this payment
        /// [Optional]
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// A set of key-value pairs that you can attach to the refund request. It can be useful for storing additional
        /// information in a structured format. Note: This object only allows one level of depth, so cannot accept
        /// non-primitive data types such as objects or arrays.
        /// [Optional]
        /// </summary>
        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// The previous related payment identifier. This could be the ID of the payment that you want to refund.
        /// [Optional]
        /// </summary>
        public string PreviousPaymentId { get; set; }

        /// <summary>
        /// The dimension details about business segment for payment request. At least one dimension required.
        /// [Optional]
        /// </summary>
        public Segment.Segment Segment { get; set; }

        /// <summary>
        /// Returns information related to the processing of the payment.
        /// [Optional]
        /// </summary>
        public Processing.Processing Processing { get; set; }
    }
}
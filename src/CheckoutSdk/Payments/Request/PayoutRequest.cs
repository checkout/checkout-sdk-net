using Checkout.Common;
using Checkout.Payments.Request.Destination;
using Checkout.Payments.Request.Source;
using Checkout.Payments.Sender;
using System.Collections.Generic;

namespace Checkout.Payments.Request
{
    public class PayoutRequest 
    {
        /// <summary>
        /// The source of the payout.
        /// [Required]
        /// </summary>
        public PayoutRequestSource Source { get; set; }

        /// <summary>
        /// The destination of the payout.
        /// [Required]
        /// </summary>
        public PaymentRequestDestination Destination { get; set; }

        /// <summary>
        /// The amount to pay out in minor currency units.
        /// [Optional]
        /// min 0
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// The three-letter ISO 4217 currency code for the destination currency.
        /// The currency should match that of the destination account.
        /// [Optional]
        /// min 3 characters
        /// max 3 characters
        /// </summary>
        public Currency? Currency { get; set; }

        /// <summary>
        /// A custom reference to identify the payout. For example, an order number.
        /// [Optional]
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Billing descriptor to display on the beneficiary's bank statement.
        /// [Optional]
        /// </summary>
        public PayoutBillingDescriptor BillingDescriptor { get; set; }

        /// <summary>
        /// The sender of the payout.
        /// [Optional]
        /// </summary>
        public PaymentSender Sender { get; set; }

        /// <summary>
        /// Details about the payout instruction.
        /// [Optional]
        /// </summary>
        public PaymentInstruction Instruction { get; set; }

        /// <summary>
        /// The unique identifier for the processing channel.
        /// [Optional]
        /// Pattern: ^(pc)_(\w{26})$
        /// </summary>
        public string ProcessingChannelId { get; set; }

        /// <summary>
        /// Stores additional information about the payout with custom fields.
        /// Supports primitive data types only; objects and arrays are not supported.
        /// [Optional]
        /// </summary>
        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// The dimension details about the business segment for this payout.
        /// [Optional]
        /// </summary>
        public PaymentSegment Segment { get; set; }
    }
}
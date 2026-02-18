using System.Collections.Generic;
using Checkout.Issuing.Disputes.Common;

namespace Checkout.Issuing.Disputes.Requests
{
    /// <summary>
    /// Submit an Issuing dispute to the card scheme for processing.
    /// [Beta]
    /// </summary>
    public class SubmitDisputeRequest
    {
        /// <summary>
        /// The updated four-digit scheme-specific reason code.
        /// If not provided, Checkout.com uses the existing reason code.
        /// [Optional]
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Your evidence for the chargeback, if updated since you created the dispute.
        /// [Optional]
        /// </summary>
        public IList<DisputeEvidence> Evidence { get; set; }

        /// <summary>
        /// The updated disputed amount, in the minor unit of the transaction or representment currency.
        /// If not provided, Checkout.com uses the existing disputed amount.
        /// [Optional]
        /// </summary>
        public long? Amount { get; set; }
    }
}
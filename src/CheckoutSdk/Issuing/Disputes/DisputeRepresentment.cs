using System;
using System.Collections.Generic;

namespace Checkout.Issuing.Disputes
{
    /// <summary>
    /// Information about the representment stage of the dispute lifecycle.
    /// [Beta]
    /// </summary>
    public class DisputeRepresentment
    {
        /// <summary>
        /// The date and time when the representment was received from the merchant.
        /// Uses the following format: <code>yyyy-MM-ddTHH:mm:ss.fffZ</code>
        /// </summary>
        public DateTime? ReceivedOn { get; set; }

        /// <summary>
        /// The amount details of the representment.
        /// </summary>
        public DisputeAmount Amount { get; set; }

        /// <summary>
        /// The list of file evidence submitted by the merchant during the representment process.
        /// </summary>
        public IList<DisputeFileEvidence> Evidence { get; set; }
    }
}
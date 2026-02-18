using System;
using System.Collections.Generic;

namespace Checkout.Issuing.Disputes.Common
{
    /// <summary>
    /// Information about the pre-arbitration stage of the dispute lifecycle.
    /// [Beta]
    /// </summary>
    public class DisputePreArbitration
    {
        /// <summary>
        /// The date and time when the pre-arbitration case was submitted.
        /// Uses the following format: <code>yyyy-MM-ddTHH:mm:ss.fffZ</code>
        /// </summary>
        public DateTime? SubmittedOn { get; set; }

        /// <summary>
        /// The list of file evidence submitted with the pre-arbitration case.
        /// </summary>
        public IList<DisputeFileEvidence> Evidence { get; set; }

        /// <summary>
        /// The amount details of the pre-arbitration case.
        /// </summary>
        public DisputeAmount Amount { get; set; }

        /// <summary>
        /// Details about any reason code changes during the pre-arbitration process.
        /// </summary>
        public DisputeReasonChange ReasonChange { get; set; }

        /// <summary>
        /// The justification provided for the pre-arbitration case.
        /// </summary>
        public string Justification { get; set; }

        /// <summary>
        /// The date and time when the merchant responded to the pre-arbitration case.
        /// Uses the following format: <code>yyyy-MM-ddTHH:mm:ss.fffZ</code>
        /// </summary>
        public DateTime? MerchantRespondedOn { get; set; }

        /// <summary>
        /// The list of file evidence submitted by the merchant in response to the pre-arbitration case.
        /// </summary>
        public IList<DisputeFileEvidence> MerchantEvidence { get; set; }
    }
}
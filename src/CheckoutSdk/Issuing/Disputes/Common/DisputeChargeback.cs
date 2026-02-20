using System;
using System.Collections.Generic;

namespace Checkout.Issuing.Disputes.Common
{
    /// <summary>
    /// The dispute details at the chargeback stage.
    /// [Beta]
    /// </summary>
    public class DisputeChargeback
    {
        /// <summary>
        /// The date and time when the chargeback was successfully submitted to the card scheme, in UTC.
        /// Format – ISO 8601 code
        /// Example – 2025-01-31T10:20:30.456
        /// </summary>
        public DateTime? SubmittedOn { get; set; }

        /// <summary>
        /// The four-digit scheme-specific reason code you provide for the chargeback.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// The disputed amount, in the minor unit of the transaction currency.
        /// </summary>
        public DisputeAmount Amount { get; set; }

        /// <summary>
        /// Your evidence for the chargeback.
        /// </summary>
        public IList<DisputeFileEvidence> Evidence { get; set; }

        /// <summary>
        /// Your justification for the chargeback.
        /// </summary>
        public string Justification { get; set; }
    }
}
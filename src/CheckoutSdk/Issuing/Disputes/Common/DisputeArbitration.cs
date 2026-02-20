using System;

namespace Checkout.Issuing.Disputes.Common
{
    /// <summary>
    /// Information about the arbitration stage of the dispute lifecycle.
    /// [Beta]
    /// </summary>
    public class DisputeArbitration
    {
        /// <summary>
        /// The date and time when the arbitration case was submitted to the card scheme.
        /// Uses the following format: <code>yyyy-MM-ddTHH:mm:ss.fffZ</code>
        /// </summary>
        public DateTime? SubmittedOn { get; set; }

        /// <summary>
        /// The amount details of the arbitration case.
        /// </summary>
        public DisputeAmount Amount { get; set; }

        /// <summary>
        /// The justification provided for the arbitration case.
        /// Explains why the dispute should proceed to arbitration and the evidence supporting the case.
        /// </summary>
        public string Justification { get; set; }

        /// <summary>
        /// The date and time when the arbitration decision was made.
        /// Uses the following format: <code>yyyy-MM-ddTHH:mm:ss.fffZ</code>
        /// </summary>
        public DateTime? DecidedOn { get; set; }
    }
}
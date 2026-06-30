using System.Collections.Generic;
using Checkout.Issuing.Disputes.Common;

namespace Checkout.Issuing.Disputes.Requests
{
    /// <summary>
    /// Escalate an Issuing dispute to pre-arbitration or arbitration.
    /// [Beta]
    /// </summary>
    public class EscalateDisputeRequest
    {
        /// <summary>
        /// Justification for escalating the dispute.
        /// [Required]
        /// &lt;= 13000 characters
        /// </summary>
        public string Justification { get; set; }

        /// <summary>
        /// Your evidence for escalating the dispute, in line with the card scheme's requirements.
        /// If the request goes to arbitration, the card scheme ignores any evidence you provide at this stage using this request.
        /// [Optional]
        /// </summary>
        public IList<DisputeEvidence> AdditionalEvidence { get; set; }

        /// <summary>
        /// The updated disputed amount, in the minor unit of the representment currency.
        /// [Optional]
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// The change to the dispute reason and your justification for changing it.
        /// [Optional]
        /// </summary>
        public DisputeReasonChange ReasonChange { get; set; }

        /// <summary>
        /// Provides fraud-related details.
        /// This field is required if the dispute has a fraud-related reason code at the escalation stage,
        /// or after a requested reason code change to a fraud code.
        /// [Optional]
        /// </summary>
        public IssuingDisputeFraudDetails FraudDetails { get; set; }
    }
}
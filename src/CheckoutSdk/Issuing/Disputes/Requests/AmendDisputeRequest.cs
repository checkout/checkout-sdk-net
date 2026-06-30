using System.Collections.Generic;
using Checkout.Issuing.Disputes.Common;

namespace Checkout.Issuing.Disputes.Requests
{
    /// <summary>
    /// Submit an amendment to a dispute that is currently blocked from proceeding. This endpoint handles
    /// both chargeback-stage and escalation-stage amendments using the same flat payload, as the server
    /// determines the context from the dispute's current state.
    /// If <c>reason</c> specifies a fraud-related dispute, you must provide <c>fraud_details</c>.
    /// If you change the <c>reason</c> at the escalation stage, you must provide <c>reason_change_justification</c>.
    /// [Beta]
    /// </summary>
    public class AmendDisputeRequest
    {
        /// <summary>
        /// The updated four-digit scheme-specific reason code.
        /// If a value is not provided, the existing reason code is retained.
        /// [Optional]
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// The updated disputed amount, in the minor unit of the transaction currency.
        /// If not provided, the existing amount is retained.
        /// [Optional]
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// The updated or additional evidence requested by the Dispute Resolution team.
        /// Follow the card scheme's requirements.
        /// [Optional]
        /// </summary>
        public IList<DisputeEvidence> Evidence { get; set; }

        /// <summary>
        /// Provides the fraud category, and additional context if available.
        /// This field is required if <c>reason</c> specifies a fraud-related dispute.
        /// [Optional]
        /// </summary>
        public IssuingDisputeFraudDetails FraudDetails { get; set; }

        /// <summary>
        /// Explains the justification for the reason change. This is shared with the Dispute Resolution
        /// review team and may be submitted to the card scheme.
        /// This field is required if you change the <c>reason</c> at the escalation stage.
        /// [Optional]
        /// &lt;= 13000 characters
        /// </summary>
        public string ReasonChangeJustification { get; set; }

        /// <summary>
        /// Free-form text that you can use to explain your choices, provide additional context, or ask
        /// questions about the requested changes.
        /// [Optional]
        /// &lt;= 1000 characters
        /// </summary>
        public string ActionResponse { get; set; }
    }
}

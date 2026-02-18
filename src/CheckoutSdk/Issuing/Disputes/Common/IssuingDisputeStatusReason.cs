using System.Runtime.Serialization;

namespace Checkout.Issuing.Disputes.Common
{
    /// <summary>
    /// The reason for the current status of the issuing dispute.
    /// [Beta]
    /// </summary>
    public enum IssuingDisputeStatusReason
    {
        /// <summary>
        /// The dispute deadline has expired without action.
        /// </summary>
        [EnumMember(Value = "expired")] Expired,
        
        /// <summary>
        /// The chargeback is pending processing by the card scheme.
        /// </summary>
        [EnumMember(Value = "chargeback_pending")] ChargebackPending,
        
        /// <summary>
        /// The chargeback evidence provided is invalid or insufficient.
        /// </summary>
        [EnumMember(Value = "chargeback_evidence_invalid_or_insufficient")] ChargebackEvidenceInvalidOrInsufficient,
        
        /// <summary>
        /// The chargeback has been successfully processed.
        /// </summary>
        [EnumMember(Value = "chargeback_processed")] ChargebackProcessed,
        
        /// <summary>
        /// The chargeback has been rejected by the card scheme.
        /// </summary>
        [EnumMember(Value = "chargeback_rejected")] ChargebackRejected,
        
        /// <summary>
        /// The chargeback reversal is pending processing.
        /// </summary>
        [EnumMember(Value = "chargeback_reversal_pending")] ChargebackReversalPending,
        
        /// <summary>
        /// The chargeback has been reversed, funds returned to the merchant.
        /// </summary>
        [EnumMember(Value = "chargeback_reversed")] ChargebackReversed,
        
        /// <summary>
        /// The chargeback response has been accepted by the card scheme.
        /// </summary>
        [EnumMember(Value = "chargeback_response_accepted")] ChargebackResponseAccepted,
        
        /// <summary>
        /// The pre-arbitration case is pending processing by the card scheme.
        /// </summary>
        [EnumMember(Value = "prearbitration_pending")] PrearbitrationPending,
        
        /// <summary>
        /// The pre-arbitration evidence provided is invalid or insufficient.
        /// </summary>
        [EnumMember(Value = "prearbitration_evidence_invalid_or_insufficient")] PrearbitrationEvidenceInvalidOrInsufficient,
        
        /// <summary>
        /// The pre-arbitration case has been successfully processed.
        /// </summary>
        [EnumMember(Value = "prearbitration_processed")] PrearbitrationProcessed,
        
        /// <summary>
        /// The pre-arbitration case has been rejected by the card scheme.
        /// </summary>
        [EnumMember(Value = "prearbitration_rejected")] PrearbitrationRejected,
        
        /// <summary>
        /// The pre-arbitration reversal is pending processing.
        /// </summary>
        [EnumMember(Value = "prearbitration_reversal_pending")] PrearbitrationReversalPending,
        
        /// <summary>
        /// The pre-arbitration case has been reversed, funds returned to the merchant.
        /// </summary>
        [EnumMember(Value = "prearbitration_reversed")] PrearbitrationReversed,
        
        /// <summary>
        /// The pre-arbitration response has been accepted by the card scheme.
        /// </summary>
        [EnumMember(Value = "prearbitration_response_accepted")] PrearbitrationResponseAccepted,
        
        /// <summary>
        /// The arbitration case is pending processing by the card scheme.
        /// </summary>
        [EnumMember(Value = "arbitration_pending")] ArbitrationPending,
        
        /// <summary>
        /// The arbitration case has been successfully processed.
        /// </summary>
        [EnumMember(Value = "arbitration_processed")] ArbitrationProcessed,
        
        /// <summary>
        /// The presentment has been reversed.
        /// </summary>
        [EnumMember(Value = "presentment_reversed")] PresentmentReversed
    }
}
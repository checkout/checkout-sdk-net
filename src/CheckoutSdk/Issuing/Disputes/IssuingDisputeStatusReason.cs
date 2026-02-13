using System.Runtime.Serialization;

namespace Checkout.Issuing.Disputes
{
    public enum IssuingDisputeStatusReason
    {
        [EnumMember(Value = "expired")] Expired,
        [EnumMember(Value = "chargeback_pending")] ChargebackPending,
        [EnumMember(Value = "chargeback_evidence_invalid_or_insufficient")] ChargebackEvidenceInvalidOrInsufficient,
        [EnumMember(Value = "chargeback_processed")] ChargebackProcessed,
        [EnumMember(Value = "chargeback_rejected")] ChargebackRejected,
        [EnumMember(Value = "chargeback_reversal_pending")] ChargebackReversalPending,
        [EnumMember(Value = "chargeback_reversed")] ChargebackReversed,
        [EnumMember(Value = "chargeback_response_accepted")] ChargebackResponseAccepted,
        [EnumMember(Value = "prearbitration_pending")] PrearbitrationPending,
        [EnumMember(Value = "prearbitration_evidence_invalid_or_insufficient")] PrearbitrationEvidenceInvalidOrInsufficient,
        [EnumMember(Value = "prearbitration_processed")] PrearbitrationProcessed,
        [EnumMember(Value = "prearbitration_rejected")] PrearbitrationRejected,
        [EnumMember(Value = "prearbitration_reversal_pending")] PrearbitrationReversalPending,
        [EnumMember(Value = "prearbitration_reversed")] PrearbitrationReversed,
        [EnumMember(Value = "prearbitration_response_accepted")] PrearbitrationResponseAccepted,
        [EnumMember(Value = "arbitration_pending")] ArbitrationPending,
        [EnumMember(Value = "arbitration_processed")] ArbitrationProcessed,
        [EnumMember(Value = "presentment_reversed")] PresentmentReversed
    }
}
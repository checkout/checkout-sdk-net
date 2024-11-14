using System.Runtime.Serialization;

namespace Checkout.Disputes
{
    public enum DisputeStatus
    {
        [EnumMember(Value = "won")]
        Won,
        
        [EnumMember(Value = "lost")]
        Lost,
        
        [EnumMember(Value = "expired")]
        Expired,
        
        [EnumMember(Value = "accepted")]
        Accepted,
        
        [EnumMember(Value = "canceled")]
        Canceled,
        
        [EnumMember(Value = "resolved")]
        Resolved,

        [EnumMember(Value = "arbitration_won")]
        ArbitrationWon,

        [EnumMember(Value = "arbitration_lost")]
        ArbitrationLost,

        [EnumMember(Value = "evidence_required")]
        EvidenceRequired,

        [EnumMember(Value = "evidence_under_review")]
        EvidenceUnderReview,

        [EnumMember(Value = "arbitration_under_review")]
        ArbitrationUnderReview,
        
        [EnumMember(Value = "arb_evidence_submitted")]
        ArbitrationEvidenceSubmitted,
    }
}
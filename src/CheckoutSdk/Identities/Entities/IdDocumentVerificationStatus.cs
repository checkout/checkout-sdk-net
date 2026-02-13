using System.Runtime.Serialization;

namespace Checkout.Identities.Entities
{
    public enum IdDocumentVerificationStatus
    {
        [EnumMember(Value = "created")]
        Created,
        
        [EnumMember(Value = "quality_checks_in_progress")]
        QualityChecksInProgress,
        
        [EnumMember(Value = "checks_in_progress")]
        ChecksInProgress,
        
        [EnumMember(Value = "approved")]
        Approved,
        
        [EnumMember(Value = "declined")]
        Declined,
        
        [EnumMember(Value = "retry_required")]
        RetryRequired,
        
        [EnumMember(Value = "inconclusive")]
        Inconclusive
    }
}
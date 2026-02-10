using System.Runtime.Serialization;

namespace Checkout.Identities.Entities
{
    public enum IdDocumentVerificationAttemptStatus
    {
        [EnumMember(Value = "checks_in_progress")]
        ChecksInProgress,
        
        [EnumMember(Value = "checks_inconclusive")]
        ChecksInconclusive,
        
        [EnumMember(Value = "completed")]
        Completed,
        
        [EnumMember(Value = "quality_checks_aborted")]
        QualityChecksAborted,
        
        [EnumMember(Value = "quality_checks_in_progress")]
        QualityChecksInProgress,
        
        [EnumMember(Value = "terminated")]
        Terminated
    }
}
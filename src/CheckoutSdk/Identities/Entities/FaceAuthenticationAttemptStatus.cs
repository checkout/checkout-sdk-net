using System.Runtime.Serialization;

namespace Checkout.Identities.Entities
{
    public enum FaceAuthenticationAttemptStatus
    {   
        [EnumMember(Value = "capture_aborted")]
        CaptureAborted,
        [EnumMember(Value = "capture_in_progress")]
        CaptureInProgress,
        [EnumMember(Value = "checks_inconclusive")]
        ChecksInconclusive,
        [EnumMember(Value = "checks_in_progress")]
        ChecksInProgress,
        [EnumMember(Value = "completed")]
        Completed,
        [EnumMember(Value = "expired")]
        Expired,
        [EnumMember(Value = "pending_redirection")]
        PendingRedirection,
        [EnumMember(Value = "capture_refused")]
        CaptureRefused
    }
}



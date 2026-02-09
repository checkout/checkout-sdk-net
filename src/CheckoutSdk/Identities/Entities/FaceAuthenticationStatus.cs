using System.Runtime.Serialization;

namespace Checkout.Identities.Entities
{
    public enum FaceAuthenticationStatus
    {
        [EnumMember(Value = "approved")]
        Approved,
        [EnumMember(Value = "capture_in_progress")]
        CaptureInProgress,
        [EnumMember(Value = "checks_in_progress")]
        ChecksInProgress,
        [EnumMember(Value = "created")]
        Created,
        [EnumMember(Value = "declined")]
        Declined,
        [EnumMember(Value = "inconclusive")]
        Inconclusive,
        [EnumMember(Value = "pending")]
        Pending,
        [EnumMember(Value = "refused")]
        Refused,
        [EnumMember(Value = "retry_required")]
        RetryRequired
    }
}



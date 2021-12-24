using System.Runtime.Serialization;

namespace Checkout.Sessions
{
    public enum SessionStatus
    {
        [EnumMember(Value = "pending")] Pending,
        [EnumMember(Value = "processing")] Processing,
        [EnumMember(Value = "challenged")] Challenged,
        [EnumMember(Value = "challenge_abandoned")] ChallengedAbandoned,
        [EnumMember(Value = "expired")] Expired,
        [EnumMember(Value = "approved")] Approved,
        [EnumMember(Value = "attempted")] Attempted,
        [EnumMember(Value = "unavailable")] Unavailable,
        [EnumMember(Value = "declined")] Declined,
        [EnumMember(Value = "rejected")] Rejected
    }
}
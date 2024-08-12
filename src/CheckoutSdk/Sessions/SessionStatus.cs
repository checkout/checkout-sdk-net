using System.Runtime.Serialization;

namespace Checkout.Sessions
{
    public enum SessionStatus
    {
        [EnumMember(Value = "approved")] Approved,
        [EnumMember(Value = "attempted")] Attempted,

        [EnumMember(Value = "challenge_abandoned")]
        ChallengeAbandoned,
        
        [EnumMember(Value = "challenged")] Challenged,
        [EnumMember(Value = "declined")] Declined,
        [EnumMember(Value = "expired")] Expired,
        [EnumMember(Value = "pending")] Pending,
        [EnumMember(Value = "processing")] Processing,
        [EnumMember(Value = "rejected")] Rejected,
        [EnumMember(Value = "unavailable")] Unavailable
    }
}
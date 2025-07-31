using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.Responses
{
    public enum StatusType
    {
        [EnumMember(Value = "challenged")]
        Challenged,

        [EnumMember(Value = "pending")]
        Pending,

        [EnumMember(Value = "processing")]
        Processing,

        [EnumMember(Value = "challenge_abandoned")]
        ChallengeAbandoned,

        [EnumMember(Value = "expired")]
        Expired,

        [EnumMember(Value = "approved")]
        Approved,

        [EnumMember(Value = "attempted")]
        Attempted,

        [EnumMember(Value = "unavailable")]
        Unavailable,

        [EnumMember(Value = "declined")]
        Declined,

        [EnumMember(Value = "rejected")]
        Rejected,
    }
}
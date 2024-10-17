using System.Runtime.Serialization;

namespace Checkout.Common
{
    public enum ChallengeIndicatorType
    {
        [EnumMember(Value = "challenge_requested")]
        ChallengeRequested,

        [EnumMember(Value = "challenge_requested_mandate")]
        ChallengeRequestedMandate,

        [EnumMember(Value = "no_challenge_requested")]
        NoChallengeRequested,
        
        [EnumMember(Value = "no_preference")]
        NoPreference,

        
    }
}
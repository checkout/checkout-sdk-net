using System.Runtime.Serialization;

namespace Checkout.Risk.PreAuthentication
{
    public enum PreAuthenticationDecision
    {
        [EnumMember(Value = "try_exemptions")] TryExemptions,

        [EnumMember(Value = "try_frictionless")]
        TryFrictionless,

        [EnumMember(Value = "no_preference")] NoPreference,

        [EnumMember(Value = "force_challenge")]
        ForceChallenge,

        [EnumMember(Value = "decline")] Decline
    }
}
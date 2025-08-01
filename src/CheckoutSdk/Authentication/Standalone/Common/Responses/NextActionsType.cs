using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.Responses
{
    public enum NextActionsType
    {
        [EnumMember(Value = "redirect_cardholder")]
        RedirectCardholder,

        [EnumMember(Value = "collect_channel_data")]
        CollectChannelData,

        [EnumMember(Value = "issuer_fingerprint")]
        IssuerFingerprint,

        [EnumMember(Value = "challenge_cardholder")]
        ChallengeCardholder,

        [EnumMember(Value = "complete")]
        Complete,
    }
}
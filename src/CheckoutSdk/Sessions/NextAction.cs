using System.Runtime.Serialization;

namespace Checkout.Sessions
{
    public enum NextAction
    {
        [EnumMember(Value = "collect_channel_data")]
        CollectChannelData,

        [EnumMember(Value = "issuer_fingerprint")]
        IssueFingerprint,

        [EnumMember(Value = "challenge_cardholder")]
        ChallengeCardHolder,

        [EnumMember(Value = "redirect_cardholder")]
        RedirectCardholder,
        [EnumMember(Value = "complete")] Complete,
        [EnumMember(Value = "authenticate")] Authenticate
    }
}
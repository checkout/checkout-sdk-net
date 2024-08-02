using System.Runtime.Serialization;

namespace Checkout.Sessions
{
    public enum NextAction
    {
        [EnumMember(Value = "authenticate")] Authenticate,

        [EnumMember(Value = "challenge_cardholder")]
        ChallengeCardholder,

        [EnumMember(Value = "collect_channel_data")]
        CollectChannelData,
        
        [EnumMember(Value = "complete")] Complete,

        [EnumMember(Value = "issuer_fingerprint")]
        IssueFingerprint,

        [EnumMember(Value = "redirect_cardholder")]
        RedirectCardholder
    }
}
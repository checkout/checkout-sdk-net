using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest
{
    public enum ChallengeIndicatorType
    {
        [EnumMember(Value = "no_preference")]
        NoPreference,

        [EnumMember(Value = "no_challenge_requested")]
        NoChallengeRequested,

        [EnumMember(Value = "challenge_requested")]
        ChallengeRequested,

        [EnumMember(Value = "challenge_requested_mandate")]
        ChallengeRequestedMandate,

        [EnumMember(Value = "low_value")]
        LowValue,

        [EnumMember(Value = "trusted_listing")]
        TrustedListing,

        [EnumMember(Value = "trusted_listing_prompt")]
        TrustedListingPrompt,

        [EnumMember(Value = "transaction_risk_assessment")]
        TransactionRiskAssessment,

        [EnumMember(Value = "data_share")]
        DataShare,
    }
}
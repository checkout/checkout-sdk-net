using System.Runtime.Serialization;

namespace Checkout.Common
{
    public enum ChallengeIndicatorType
    {
        [EnumMember(Value = "challenge_requested")]
        ChallengeRequested,

        [EnumMember(Value = "challenge_requested_mandate")]
        ChallengeRequestedMandate,
        
        [EnumMember(Value = "data_share")]
        DataShare,
        
        [EnumMember(Value = "low_value")] 
        LowValue,

        [EnumMember(Value = "no_challenge_requested")]
        NoChallengeRequested,
        
        [EnumMember(Value = "no_preference")]
        NoPreference,

        [EnumMember(Value = "transaction_risk_assessment")]
        TransactionRiskAssessment,

        [EnumMember(Value = "trusted_listing")]
        TrustedListing,

        [EnumMember(Value = "trusted_listing_prompt")]
        TrustedListingPrompt
    }
}
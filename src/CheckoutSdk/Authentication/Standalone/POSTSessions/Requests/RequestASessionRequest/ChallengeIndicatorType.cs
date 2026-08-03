using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest
{
    /// <summary>
    /// Indicates whether a challenge is requested for this session.
    /// Used by RequestASessionRequest.ChallengeIndicator for POST /sessions.
    /// This is the only field in the API that accepts the exemption values below; the
    /// 3ds.challenge_indicator field on payments, hosted payments, payment links and payment
    /// sessions accepts only the first four values and is modelled by
    /// Checkout.Common.ChallengeIndicatorType.
    /// The following are requests for exemption: LowValue, TrustedListing, TrustedListingPrompt and
    /// TransactionRiskAssessment. If an exemption cannot be applied, then the value
    /// NoChallengeRequested will be used instead.
    /// [Optional]
    /// Default: NoPreference
    /// max 50 characters
    /// </summary>
    public enum ChallengeIndicatorType
    {
        /// <summary>
        /// No preference as to whether a challenge should be performed. This is the default.
        /// </summary>
        [EnumMember(Value = "no_preference")]
        NoPreference,

        /// <summary>
        /// A challenge is not requested for this session.
        /// </summary>
        [EnumMember(Value = "no_challenge_requested")]
        NoChallengeRequested,

        /// <summary>
        /// A challenge is requested for this session.
        /// </summary>
        [EnumMember(Value = "challenge_requested")]
        ChallengeRequested,

        /// <summary>
        /// A challenge is requested for this session because it is mandated by local regulation or
        /// scheme rules.
        /// </summary>
        [EnumMember(Value = "challenge_requested_mandate")]
        ChallengeRequestedMandate,

        /// <summary>
        /// Request a low-value exemption. If the exemption cannot be applied, the value
        /// NoChallengeRequested will be used instead.
        /// </summary>
        [EnumMember(Value = "low_value")]
        LowValue,

        /// <summary>
        /// Request a trusted listing exemption, applied when the cardholder has already added the
        /// merchant to their list of trusted beneficiaries. If the exemption cannot be applied, the
        /// value NoChallengeRequested will be used instead.
        /// </summary>
        [EnumMember(Value = "trusted_listing")]
        TrustedListing,

        /// <summary>
        /// Request a trusted listing exemption and prompt the cardholder to add the merchant to
        /// their list of trusted beneficiaries. If the exemption cannot be applied, the value
        /// NoChallengeRequested will be used instead.
        /// </summary>
        [EnumMember(Value = "trusted_listing_prompt")]
        TrustedListingPrompt,

        /// <summary>
        /// Request a transaction risk analysis (TRA) exemption. If the exemption cannot be applied,
        /// the value NoChallengeRequested will be used instead.
        /// </summary>
        [EnumMember(Value = "transaction_risk_assessment")]
        TransactionRiskAssessment,

        /// <summary>
        /// Request a data-share authentication, where cardholder data is shared with the issuer to
        /// support their risk assessment without requesting a challenge.
        /// </summary>
        [EnumMember(Value = "data_share")]
        DataShare,
    }
}

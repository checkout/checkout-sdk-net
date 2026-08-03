using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.Responses
{
    /// <summary>
    /// Indicates the preference for whether or not a 3DS challenge should be performed. The
    /// customer's bank has the final say on whether or not the customer receives the challenge.
    /// Returned by the session responses.
    /// The API Reference specifies only the first four values for the session response fields, but
    /// the request accepts all nine. The wider set is modelled here deliberately so that an
    /// exemption value echoed back by the API still deserializes rather than being dropped.
    /// [Required]
    /// Default: NoPreference
    /// </summary>
    public enum ChallengeIndicatorType
    {
        /// <summary>
        /// No preference as to whether a challenge should be performed. This is the default.
        /// </summary>
        [EnumMember(Value = "no_preference")]
        NoPreference,

        /// <summary>
        /// A challenge was not requested for this session.
        /// </summary>
        [EnumMember(Value = "no_challenge_requested")]
        NoChallengeRequested,

        /// <summary>
        /// A challenge was requested for this session.
        /// </summary>
        [EnumMember(Value = "challenge_requested")]
        ChallengeRequested,

        /// <summary>
        /// A challenge was requested for this session because it is mandated by local regulation or
        /// scheme rules.
        /// </summary>
        [EnumMember(Value = "challenge_requested_mandate")]
        ChallengeRequestedMandate,

        /// <summary>
        /// A low-value exemption was requested. Only returned when it was requested on the session.
        /// </summary>
        [EnumMember(Value = "low_value")]
        LowValue,

        /// <summary>
        /// A trusted listing exemption was requested. Only returned when it was requested on the
        /// session.
        /// </summary>
        [EnumMember(Value = "trusted_listing")]
        TrustedListing,

        /// <summary>
        /// A trusted listing exemption with a cardholder prompt was requested. Only returned when it
        /// was requested on the session.
        /// </summary>
        [EnumMember(Value = "trusted_listing_prompt")]
        TrustedListingPrompt,

        /// <summary>
        /// A transaction risk analysis (TRA) exemption was requested. Only returned when it was
        /// requested on the session.
        /// </summary>
        [EnumMember(Value = "transaction_risk_assessment")]
        TransactionRiskAssessment,

        /// <summary>
        /// A data-share authentication was requested. Only returned when it was requested on the
        /// session.
        /// </summary>
        [EnumMember(Value = "data_share")]
        DataShare
    }
}

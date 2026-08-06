using System.Runtime.Serialization;

namespace Checkout.Common
{
    /// <summary>
    /// Indicates the preference for whether or not a 3DS challenge should be performed. The
    /// customer's bank has the final say on whether or not the customer receives the challenge.
    /// This is the four-value indicator accepted by the 3ds.challenge_indicator field on
    /// POST /payments, POST /hosted-payments, POST /payment-links and POST /payment-sessions.
    /// For POST /sessions, which additionally supports requests for exemption, use
    /// Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ChallengeIndicatorType.
    /// [Optional]
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
        /// A challenge is not requested for this payment.
        /// </summary>
        [EnumMember(Value = "no_challenge_requested")]
        NoChallengeRequested,

        /// <summary>
        /// A challenge is requested for this payment.
        /// </summary>
        [EnumMember(Value = "challenge_requested")]
        ChallengeRequested,

        /// <summary>
        /// A challenge is requested for this payment because it is mandated by local regulation or
        /// scheme rules.
        /// </summary>
        [EnumMember(Value = "challenge_requested_mandate")]
        ChallengeRequestedMandate,
    }
}

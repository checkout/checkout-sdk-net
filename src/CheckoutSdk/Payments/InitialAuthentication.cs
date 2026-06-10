using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum InitialAuthenticationMethod
    {
        [EnumMember(Value = "frictionless_authentication")]
        FrictionlessAuthentication,

        [EnumMember(Value = "challenge_occurred")]
        ChallengeOccurred,

        [EnumMember(Value = "avs_verified")]
        AvsVerified,

        [EnumMember(Value = "other_issuer_methods")]
        OtherIssuerMethods,
    }

    /// <summary>
    /// The details of a previous 3DS authenticated transaction.
    /// </summary>
    public class InitialAuthentication
    {
        /// <summary>
        /// The Access Control Server (ACS) transaction ID for a previously authenticated transaction.
        /// [Optional]
        /// min 36 characters
        /// </summary>
        public string AcsTransactionId { get; set; }

        /// <summary>
        /// The authentication method used in the previous transaction.
        /// [Optional]
        /// </summary>
        public InitialAuthenticationMethod? AuthenticationMethod { get; set; }

        /// <summary>
        /// The timestamp of the previous authentication, in ISO 8601 format.
        /// [Optional]
        /// Format: date-time
        /// </summary>
        public string AuthenticationTimestamp { get; set; }

        /// <summary>
        /// The data that documents and supports a specific authentication process.
        /// [Optional]
        /// max 2048 characters
        /// </summary>
        public string AuthenticationData { get; set; }

        /// <summary>
        /// The ID for a previous session, used for retrieving the initial transaction's properties.
        /// [Optional]
        /// min 30 characters
        /// max 30 characters
        /// </summary>
        public string InitialSessionId { get; set; }
    }
}

using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum ThreeDsReqAuthMethod
    {
        [EnumMember(Value = "no_threeds_requestor_authentication_occurred")]
        NoThreeDsRequestorAuthenticationOccurred,

        [EnumMember(Value = "three3ds_requestor_own_credentials")]
        ThreeDsRequestorOwnCredentials,

        [EnumMember(Value = "federated_id")]
        FederatedId,

        [EnumMember(Value = "issuer_credentials")]
        IssuerCredentials,

        [EnumMember(Value = "third_party_authentication")]
        ThirdPartyAuthentication,

        [EnumMember(Value = "fido_authenticator")]
        FidoAuthenticator,

        [EnumMember(Value = "fido_authenticator_fido_assurance_data_signed")]
        FidoAuthenticatorFidoAssuranceDataSigned,

        [EnumMember(Value = "src_assurance_data")]
        SrcAssuranceData,
    }

    /// <summary>
    /// The information about how the 3DS Requestor authenticated the cardholder
    /// before or during the transaction.
    /// </summary>
    public class MerchantAuthenticationInfo
    {
        /// <summary>
        /// The mechanism used by the cardholder to authenticate with the 3DS Requestor.
        /// [Optional]
        /// </summary>
        public ThreeDsReqAuthMethod? ThreeDsReqAuthMethod { get; set; }

        /// <summary>
        /// The UTC date and time the cardholder authenticated with the 3DS Requestor, in ISO 8601 format.
        /// [Optional]
        /// Format: date-time
        /// </summary>
        public string ThreeDsReqAuthTimestamp { get; set; }

        /// <summary>
        /// The data that documents and supports a specific authentication process.
        /// [Optional]
        /// max 20000 characters
        /// </summary>
        public string ThreeDsReqAuthData { get; set; }
    }
}

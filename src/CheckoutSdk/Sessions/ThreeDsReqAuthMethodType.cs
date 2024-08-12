using System.Runtime.Serialization;

namespace Checkout.Sessions
{
    public enum ThreeDsReqAuthMethodType
    {
        [EnumMember(Value = "federated_id")] 
        FederatedId,

        [EnumMember(Value = "fido_authenticator")]
        FidoAuthenticator,

        [EnumMember(Value = "fido_authenticator_fido_assurance_data_signed")]
        FidoAuthenticatorFidoAssuranceDataSigned,

        [EnumMember(Value = "issuer_credentials")]
        IssuerCredentials,

        [EnumMember(Value = "no_threeds_requestor_authentication_occurred")]
        NoThreedsRequestorAuthenticationOccurred,

        [EnumMember(Value = "src_assurance_data")]
        SrcAssuranceData,

        [EnumMember(Value = "three3ds_requestor_own_credentials")]
        Three3dsRequestorOwnCredentials,

        [EnumMember(Value = "third_party_authentication")]
        ThirdPartyAuthentication
    }
}
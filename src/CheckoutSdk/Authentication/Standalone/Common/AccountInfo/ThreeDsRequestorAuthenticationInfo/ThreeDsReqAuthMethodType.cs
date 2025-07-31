using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.AccountInfo.ThreeDsRequestorAuthenticationInfo
{
    public enum ThreeDsReqAuthMethodType
    {
        [EnumMember(Value = "no_threeds_requestor_authentication_occurred")]
        NoThreedsRequestorAuthenticationOccurred,

        [EnumMember(Value = "three3ds_requestor_own_credentials")]
        ThreeThreedsRequestorOwnCredentials,

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
}
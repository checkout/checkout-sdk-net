using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.AccountInfo
{
    public enum AuthenticationMethodType
    {
        [EnumMember(Value = "no_authentication")]
        NoAuthentication,

        [EnumMember(Value = "own_credentials")]
        OwnCredentials,

        [EnumMember(Value = "federated_id")]
        FederatedId,

        [EnumMember(Value = "issuer_credentials")]
        IssuerCredentials,

        [EnumMember(Value = "third_party_authentication")]
        ThirdPartyAuthentication,

        [EnumMember(Value = "fido")]
        Fido,
    }
}
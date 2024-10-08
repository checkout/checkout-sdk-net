using System.Runtime.Serialization;

namespace Checkout.Sessions
{
    public enum AuthenticationMethod
    {
        [EnumMember(Value = "federated_id")] FederatedId,
        [EnumMember(Value = "fido")] Fido,

        [EnumMember(Value = "issuer_credentials")]
        IssuerCredentials,

        [EnumMember(Value = "no_authentication")]
        NoAuthentication,

        [EnumMember(Value = "own_credentials")]
        OwnCredentials,

        [EnumMember(Value = "third_party_authentication")]
        ThirdPartyAuthentication
    }
}
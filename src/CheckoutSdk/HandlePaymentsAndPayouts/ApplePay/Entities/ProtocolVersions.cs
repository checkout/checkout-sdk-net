using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.ApplePay.Entities
{
    public enum ProtocolVersions
    {
        [EnumMember(Value = "ec_v1")]
        EcV1,
        [EnumMember(Value = "rsa_v1")]
        RsaV1
    }
}
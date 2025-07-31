using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.Responses.Exemption.TrustedBeneficiary
{
    public enum StatusType
    {
        [EnumMember(Value = "Y")]
        Y,

        [EnumMember(Value = "N")]
        N,

        [EnumMember(Value = "E")]
        E,

        [EnumMember(Value = "P")]
        P,

        [EnumMember(Value = "R")]
        R,

        [EnumMember(Value = "U")]
        U,
    }
}
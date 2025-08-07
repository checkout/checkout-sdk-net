using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.Responses
{
    public enum ResponseCodeType
    {
        [EnumMember(Value = "Y")]
        Y,

        [EnumMember(Value = "N")]
        N,

        [EnumMember(Value = "U")]
        U,

        [EnumMember(Value = "A")]
        A,

        [EnumMember(Value = "C")]
        C,

        [EnumMember(Value = "D")]
        D,

        [EnumMember(Value = "R")]
        R,

        [EnumMember(Value = "I")]
        I,
    }
}
using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.Requests
{
    public enum ThreeDsMethodCompletionType
    {
        [EnumMember(Value = "Y")]
        Y,

        [EnumMember(Value = "N")]
        N,

        [EnumMember(Value = "U")]
        U,
    }
}

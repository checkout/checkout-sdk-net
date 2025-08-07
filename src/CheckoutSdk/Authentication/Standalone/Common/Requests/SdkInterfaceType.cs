using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.Requests
{
    public enum SdkInterfaceType
    {
        [EnumMember(Value = "native")]
        Native,

        [EnumMember(Value = "html")]
        Html,

        [EnumMember(Value = "both")]
        Both,
    }
}
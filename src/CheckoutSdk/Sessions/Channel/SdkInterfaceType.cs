using System.Runtime.Serialization;

namespace Checkout.Sessions.Channel
{
    public enum SdkInterfaceType
    {
        [EnumMember(Value = "native")] Native,
        [EnumMember(Value = "html")] Html,
        [EnumMember(Value = "both")] Both
    }
}
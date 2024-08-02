using System.Runtime.Serialization;

namespace Checkout.Sessions.Channel
{
    public enum SdkInterfaceType
    {
        [EnumMember(Value = "both")] Both,
        [EnumMember(Value = "html")] Html,
        [EnumMember(Value = "native")] Native
    }
}
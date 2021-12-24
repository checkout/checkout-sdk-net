using System.Runtime.Serialization;

namespace Checkout.Sessions.Channel
{
    public enum ChannelType
    {
        [EnumMember(Value = "browser")] Browser,
        [EnumMember(Value = "app")] App
    }
}
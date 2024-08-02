using System.Runtime.Serialization;

namespace Checkout.Sessions.Channel
{
    public enum ChannelType
    {
        [EnumMember(Value = "app")] App,
        [EnumMember(Value = "browser")] Browser,
        [EnumMember(Value = "merchant_initiated")] MerchantInitiated
    }
}
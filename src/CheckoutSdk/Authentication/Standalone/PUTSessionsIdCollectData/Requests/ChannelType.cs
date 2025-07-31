using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.PUTSessionsIdCollectData.Requests
{
    public enum ChannelType
    {
        [EnumMember(Value = "browser")]
        Browser,
        
        [EnumMember(Value = "app")]
        App,
        
        [EnumMember(Value = "merchant_initiated")]
        MerchantInitiated,
    }
}
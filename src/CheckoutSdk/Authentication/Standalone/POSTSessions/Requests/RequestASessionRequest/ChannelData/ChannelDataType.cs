using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ChannelData
{
    public enum ChannelDataType
    {
        [EnumMember(Value = "merchant_initiated")]
        MerchantInitiated,

        [EnumMember(Value = "app")]
        App,

        [EnumMember(Value = "browser")]
        Browser,
    }
}
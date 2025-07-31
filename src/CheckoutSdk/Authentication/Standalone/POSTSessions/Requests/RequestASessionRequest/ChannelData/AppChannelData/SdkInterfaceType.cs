using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ChannelData.AppChannelData
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
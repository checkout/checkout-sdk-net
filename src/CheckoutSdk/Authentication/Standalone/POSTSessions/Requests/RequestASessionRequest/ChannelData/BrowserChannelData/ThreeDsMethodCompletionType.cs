using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ChannelData.BrowserChannelData
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

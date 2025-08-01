using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.PUTSessionsIdCollectData.Requests.BrowserRequest
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
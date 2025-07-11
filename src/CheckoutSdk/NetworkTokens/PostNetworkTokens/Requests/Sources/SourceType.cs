using System.Runtime.Serialization;

namespace Checkout.NetworkTokens.PostNetworkTokens.Requests.Sources
{
    public enum SourceType
    {
        [EnumMember(Value = "id")]
        Id,

        [EnumMember(Value = "card")]
        Card,

    }
}

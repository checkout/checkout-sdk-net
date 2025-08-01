using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Source
{
    public enum SourceType
    {
        [EnumMember(Value = "card")]
        Card,

        [EnumMember(Value = "id")]
        Id,

        [EnumMember(Value = "token")]
        Token,

        [EnumMember(Value = "network_token")]
        NetworkToken,
    }
}
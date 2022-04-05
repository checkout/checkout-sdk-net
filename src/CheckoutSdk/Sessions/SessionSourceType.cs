using System.Runtime.Serialization;

namespace Checkout.Sessions
{
    public enum SessionSourceType
    {
        [EnumMember(Value = "card")] Card,
        [EnumMember(Value = "id")] Id,
        [EnumMember(Value = "token")] Token,
        [EnumMember(Value = "network_token")] NetworkToken
    }
}
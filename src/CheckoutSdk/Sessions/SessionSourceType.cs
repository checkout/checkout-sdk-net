using System.Runtime.Serialization;

namespace Checkout.Sessions
{
    public enum SessionSourceType
    {
        [EnumMember(Value = "card")] Card,
        [EnumMember(Value = "id")] Id,
        [EnumMember(Value = "artokenes_error")] Token
    }
}
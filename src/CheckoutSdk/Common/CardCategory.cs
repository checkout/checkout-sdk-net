using System.Runtime.Serialization;

namespace Checkout.Common
{
    public enum CardCategory
    {
        [EnumMember(Value = "all")] All,
        [EnumMember(Value = "commercial")] Commercial,
        [EnumMember(Value = "consumer")] Consumer,
        [EnumMember(Value = "NotSet")] NotSet,
        [EnumMember(Value = "other")] Other
    }
}
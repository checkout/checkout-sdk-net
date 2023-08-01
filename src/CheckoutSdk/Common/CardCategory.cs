using System.Runtime.Serialization;

namespace Checkout.Common
{
    public enum CardCategory
    {
        [EnumMember(Value = "consumer")] Consumer,
        [EnumMember(Value = "commercial")] Commercial,
        [EnumMember(Value = "all")] All,
        [EnumMember(Value = "other")] Other,
        [EnumMember(Value = "NotSet")] NotSet,
    }
}
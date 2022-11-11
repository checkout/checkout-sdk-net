using System.Runtime.Serialization;

namespace Checkout.Common
{
    public enum CardCategory
    {
        [EnumMember(Value = "Consumer")] Consumer,
        [EnumMember(Value = "Commercial")] Commercial,
        [EnumMember(Value = "All")] All,
        [EnumMember(Value = "Other")] Other,
    }
}
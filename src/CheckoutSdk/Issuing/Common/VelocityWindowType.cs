using System.Runtime.Serialization;

namespace Checkout.Issuing.Common
{
    public enum VelocityWindowType
    {
        [EnumMember(Value = "daily")] Daily,
        [EnumMember(Value = "weekly")] Weekly,
        [EnumMember(Value = "monthly")] Monthly,
        [EnumMember(Value = "all_time")] AllTime
    }
}
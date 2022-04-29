using System.Runtime.Serialization;

namespace Checkout.Marketplace.Payout
{
    public enum ScheduleFrequency
    {
        [EnumMember(Value = "weekly")] Weekly,
        [EnumMember(Value = "daily")] Daily,
        [EnumMember(Value = "monthly")] Monthly
    }
}
using System.Runtime.Serialization;

namespace Checkout.Accounts.Payout
{
    public enum ScheduleFrequency
    {
        [EnumMember(Value = "weekly")] Weekly,
        [EnumMember(Value = "daily")] Daily,
        [EnumMember(Value = "monthly")] Monthly
    }
}
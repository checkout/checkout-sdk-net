using System.Runtime.Serialization;

namespace Checkout.Marketplace.Payout
{
    public enum DaySchedule
    {
        [EnumMember(Value = "Monday")] Monday,
        [EnumMember(Value = "Tuesday")] Tuesday,
        [EnumMember(Value = "Wednesday")] Wednesday,
        [EnumMember(Value = "Thursday")] Thursday,
        [EnumMember(Value = "Friday")] Friday,
        [EnumMember(Value = "Saturday")] Saturday,
        [EnumMember(Value = "Sunday")] Sunday,
    }
}
using System.Runtime.Serialization;

namespace Checkout.Sessions
{
    public enum DeliveryTimeframe
    {
        [EnumMember(Value = "electronic_delivery")] ElectronicDelivery,
        [EnumMember(Value = "same_day")] SameDay,
        [EnumMember(Value = "overnight")] Overnight,
        [EnumMember(Value = "two_day_or_more")] TwoDayOrMore,
    }
}
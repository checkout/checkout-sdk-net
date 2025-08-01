using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.MerchantRiskInfo
{
    public enum DeliveryTimeframeType
    {
        [EnumMember(Value = "electronic_delivery")]
        ElectronicDelivery,

        [EnumMember(Value = "same_day")]
        SameDay,

        [EnumMember(Value = "overnight")]
        Overnight,

        [EnumMember(Value = "two_day_or_more")]
        TwoDayOrMore,
    }
}
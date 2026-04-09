using System.Runtime.Serialization;

namespace Checkout.Issuing.DigitalCards.Responses
{
    /// <summary>
    /// The type of device associated with a digital card.
    /// </summary>
    public enum IssuingDigitalCardDeviceType
    {
        [EnumMember(Value = "host_card_emulation")]
        HostCardEmulation,
        [EnumMember(Value = "samsung_phone")]
        SamsungPhone,
        [EnumMember(Value = "samsung_tablet")]
        SamsungTablet,
        [EnumMember(Value = "samsung_watch")]
        SamsungWatch,
        [EnumMember(Value = "samsung_tv")]
        SamsungTv,
        [EnumMember(Value = "iphone")]
        Iphone,
        [EnumMember(Value = "iwatch")]
        Iwatch,
        [EnumMember(Value = "ipad")]
        Ipad,
        [EnumMember(Value = "mac_book")]
        MacBook,
        [EnumMember(Value = "android_phone")]
        AndroidPhone,
        [EnumMember(Value = "android_tablet")]
        AndroidTablet,
        [EnumMember(Value = "android_watch")]
        AndroidWatch,
        [EnumMember(Value = "mobile_phone")]
        MobilePhone,
        [EnumMember(Value = "tablet")]
        Tablet,
        [EnumMember(Value = "watch")]
        Watch,
        [EnumMember(Value = "mobile_phone_or_tablet")]
        MobilePhoneOrTablet,
        [EnumMember(Value = "bracelet")]
        Bracelet,
        [EnumMember(Value = "unknown")]
        Unknown,
    }
}

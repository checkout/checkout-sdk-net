using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.MerchantRiskInfo
{
    public enum ShippingIndicatorType
    {
        [EnumMember(Value = "billing_address")]
        BillingAddress,

        [EnumMember(Value = "another_address_on_file")]
        AnotherAddressOnFile,

        [EnumMember(Value = "not_on_file")]
        NotOnFile,

        [EnumMember(Value = "store_pick_up")]
        StorePickUp,

        [EnumMember(Value = "digital_goods")]
        DigitalGoods,

        [EnumMember(Value = "travel_and_event_no_shipping")]
        TravelAndEventNoShipping,

        [EnumMember(Value = "other")]
        Other,
    }
}
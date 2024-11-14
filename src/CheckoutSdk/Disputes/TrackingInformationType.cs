using System.Runtime.Serialization;

namespace Checkout.Disputes
{
    public enum TrackingInformationType
    {
        [EnumMember(Value = "in_transit")]
        InTransit,

        [EnumMember(Value = "partial_shipped")]
        PartialShipped,

        [EnumMember(Value = "shipped")]
        Shipped,

        [EnumMember(Value = "shipping_exception")]
        ShippingException,

        [EnumMember(Value = "delivered")]
        Delivered,

        [EnumMember(Value = "other")]
        Other,
    }
}
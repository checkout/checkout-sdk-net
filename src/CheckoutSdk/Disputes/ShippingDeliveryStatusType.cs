using System.Runtime.Serialization;

namespace Checkout.Disputes
{
    public enum ShippingDeliveryStatusType
    {
        [EnumMember(Value = "not_shipped")]
        NotShipped,
        
        [EnumMember(Value = "back_ordered")]
        BackOrdered,
        
        [EnumMember(Value = "in_transit")]
        InTransit,

        [EnumMember(Value = "partial_shipped")]
        PartialShipped,
        
        [EnumMember(Value = "shipped")]
        Shipped,
        
        [EnumMember(Value = "cancelled")]
        Cancelled,

        [EnumMember(Value = "shipping_exception")]
        ShippingException,

        [EnumMember(Value = "picked_up_by_customer")]
        PickedUpByCustomer,
        
        [EnumMember(Value = "delivered")]
        Delivered,
        
        [EnumMember(Value = "other")]
        Other,
    }
}
using System.Runtime.Serialization;

namespace Checkout.Sessions
{
    public enum PreOrderPurchaseIndicatorType
    {
        [EnumMember(Value = "future_availability")]
        FutureAvailability,
        
        [EnumMember(Value = "merchandise_available")]
        MerchandiseAvailable
    }
}
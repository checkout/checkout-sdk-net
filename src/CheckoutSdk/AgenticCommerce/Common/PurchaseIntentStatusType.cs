using System.Runtime.Serialization;

namespace Checkout.AgenticCommerce.Common
{
    public enum PurchaseIntentStatusType
    {
        [EnumMember(Value = "active")]
        Active,
        
        [EnumMember(Value = "created")]
        Created,
        
        [EnumMember(Value = "cancelled")]
        Cancelled,
        
        [EnumMember(Value = "expired")]
        Expired,
        
        [EnumMember(Value = "declined")]
        Declined,
        
        [EnumMember(Value = "completed")]
        Completed,
    }
}
using System.Runtime.Serialization;

namespace Checkout.Payments.Setups.Entities
{
    public enum PaymentMethodStatus
    {
        //"available" "requires_action" "unavailable"
        [EnumMember(Value = "available")]
        Available,
        
        [EnumMember(Value = "requires_action")]
        RequiresAction,
        
        [EnumMember(Value = "unavailable")]
        Unavailable
    }
}
using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public enum TotalPriceStatus
    {
        [EnumMember(Value = "estimated")]
        Estimated,
        
        [EnumMember(Value = "final")]
        Final
    }
}
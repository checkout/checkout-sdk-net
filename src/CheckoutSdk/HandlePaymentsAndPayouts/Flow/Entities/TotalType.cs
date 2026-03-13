using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public enum TotalType
    {
        [EnumMember(Value = "pending")]
        Pending,
        
        [EnumMember(Value = "final")]
        Final
    }
}
using System.Runtime.Serialization;

namespace Checkout.Issuing.Testing.Responses
{
    public enum ReversalStatus
    {
        [EnumMember(Value = "Reversed")] Reversed,
        
        [EnumMember(Value = "Declined")] Declined
        
    }
}
using System.Runtime.Serialization;

namespace Checkout.Issuing.Transactions.Responses
{
    public enum ReferenceType
    {
        [EnumMember(Value = "original_mit")]
        OriginalMit,
        
        [EnumMember(Value = "original_recurring")]
        OriginalRecurring,
    }
}
using System.Runtime.Serialization;

namespace Checkout.Sessions
{
    public enum ReorderItemsIndicatorType
    {
        [EnumMember(Value = "first_time_ordered")]
        FirstTimeOrdered,
        
        [EnumMember(Value = "reordered")]
        Reordered
    }
}
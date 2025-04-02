using System.Runtime.Serialization;

namespace Checkout.Issuing.Transactions.Responses
{
    public enum ResultType
    {
        [EnumMember(Value = "approved")]
        Approved,

        [EnumMember(Value = "declined")]
        Declined,

        [EnumMember(Value = "processed")]
        Processed
    }
}
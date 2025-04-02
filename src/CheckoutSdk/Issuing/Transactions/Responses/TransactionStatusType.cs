using System.Runtime.Serialization;

namespace Checkout.Issuing.Transactions.Responses
{
    public enum TransactionStatusType
    {
        [EnumMember(Value = "authorized")] Authorized,

        [EnumMember(Value = "declined")] Declined,

        [EnumMember(Value = "canceled")] Canceled,

        [EnumMember(Value = "cleared")] Cleared,

        [EnumMember(Value = "refunded")] Refunded,

        [EnumMember(Value = "disputed")] Disputed,
    }
}
using System.Runtime.Serialization;

namespace Checkout.Common
{
    public enum CardType
    {
        [EnumMember(Value = "Credit")] Credit,
        [EnumMember(Value = "Debit")] Debit,
        [EnumMember(Value = "Prepaid")] Prepaid,
        [EnumMember(Value = "Charge")] Charge,
        [EnumMember(Value = "Deferred Debit")] DeferredDebit
    }
}
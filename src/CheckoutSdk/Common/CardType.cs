using System.Runtime.Serialization;

namespace Checkout.Common
{
    public enum CardType
    {
        [EnumMember(Value = "Charge")] Charge,
        [EnumMember(Value = "Credit")] Credit,
        [EnumMember(Value = "Debit")] Debit,
        [EnumMember(Value = "Deferred Debit")] DeferredDebit,
        [EnumMember(Value = "Prepaid")] Prepaid
    }
}
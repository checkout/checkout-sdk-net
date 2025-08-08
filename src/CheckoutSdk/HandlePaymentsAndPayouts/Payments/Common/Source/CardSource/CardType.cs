using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    CardSource
{
    public enum CardType
    {
        [EnumMember(Value = "CREDIT")]
        CREDIT,

        [EnumMember(Value = "DEBIT")]
        DEBIT,

        [EnumMember(Value = "PREPAID")]
        PREPAID,

        [EnumMember(Value = "CHARGE")]
        CHARGE,

        [EnumMember(Value = "DEFERRED DEBIT")]
        DEFERREDDEBIT,
    }
}
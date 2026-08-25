using System.Runtime.Serialization;

namespace Checkout.Instruments
{
    /// <summary>
    /// The type of payment for a SEPA instrument.
    /// The wire values are lowercase. The equivalent Bacs Direct Debit field is capitalized, so do
    /// not share one type between the two. Do not replace this enum with Checkout.Payments.PaymentType
    /// either: that enum serializes capitalized values and also accepts MOTO, Installment, PayLater
    /// and Unscheduled, which SEPA does not allow.
    /// </summary>
    public enum SepaPaymentType
    {
        [EnumMember(Value = "recurring")]
        Recurring,

        [EnumMember(Value = "regular")]
        Regular,
    }
}

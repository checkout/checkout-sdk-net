using System.Runtime.Serialization;

namespace Checkout.Instruments
{
    /// <summary>
    /// The type of payment for a Bacs Direct Debit instrument.
    /// The wire values are capitalized, and the specification allows these two values only. The
    /// equivalent SEPA field is lowercase in the specification, so do not share one type between
    /// the two. Do not replace this enum with Checkout.Payments.PaymentType either: that enum also
    /// accepts MOTO, Installment, PayLater and Unscheduled, which Bacs does not allow.
    /// </summary>
    public enum BacsPaymentType
    {
        [EnumMember(Value = "Recurring")]
        Recurring,

        [EnumMember(Value = "Regular")]
        Regular,
    }
}

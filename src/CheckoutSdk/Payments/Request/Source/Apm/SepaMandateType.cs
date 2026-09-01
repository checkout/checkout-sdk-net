using System.Runtime.Serialization;

namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>
    /// The type of SEPA mandate on a SEPA payment source.
    /// This deliberately does not reuse Checkout.Instruments.SepaMandateType. The two enums carry the
    /// same two values today, but they belong to independent schemas: this one to
    /// PaymentRequestSEPAV4Source.mandate_type, the other to the SEPA instrument's
    /// instrument_data.type. The SEPA and Bacs Direct Debit payment_type fields are the precedent for
    /// two same-looking value sets diverging.
    /// Not to be confused with Checkout.Sources.Previous.MandateType, which is the previous
    /// platform's enum and carries "single" and "recurring" instead.
    /// </summary>
    public enum SepaMandateType
    {
        [EnumMember(Value = "Core")]
        Core,

        [EnumMember(Value = "B2B")]
        B2B,
    }
}

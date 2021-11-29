using System.Runtime.Serialization;

namespace Checkout.Payments.Four.Sender
{
    public enum PaymentSenderType
    {
        [EnumMember(Value = "individual")] Individual,
        [EnumMember(Value = "corporate")] Corporate,
        [EnumMember(Value = "instrument")] Instrument
    }
}
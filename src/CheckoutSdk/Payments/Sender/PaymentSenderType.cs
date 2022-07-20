using System.Runtime.Serialization;

namespace Checkout.Payments.Sender
{
    public enum PaymentSenderType
    {
        [EnumMember(Value = "individual")] Individual,
        [EnumMember(Value = "corporate")] Corporate,
        [EnumMember(Value = "instrument")] Instrument
    }
}
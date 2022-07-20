using System.Runtime.Serialization;

namespace Checkout.Instruments.Get
{
    public enum PaymentNetwork
    {
        [EnumMember(Value = "local")] Local,
        [EnumMember(Value = "sepa")] Sepa,
        [EnumMember(Value = "fps")] Fps,
        [EnumMember(Value = "ach")] Ach,
        [EnumMember(Value = "fedwire")] Fedwire,
        [EnumMember(Value = "swift")] Swift
    }
}
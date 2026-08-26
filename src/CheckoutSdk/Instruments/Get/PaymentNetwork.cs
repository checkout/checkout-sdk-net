using System.Runtime.Serialization;

namespace Checkout.Instruments.Get
{
    /// <summary>
    /// The banking network used to filter the bank account fields returned.
    /// </summary>
    public enum PaymentNetwork
    {
        [EnumMember(Value = "local")]
        Local,

        [EnumMember(Value = "sepa")]
        Sepa,

        [EnumMember(Value = "fps")]
        Fps,

        [EnumMember(Value = "ach")]
        Ach,

        [EnumMember(Value = "fedwire")]
        Fedwire,

        [EnumMember(Value = "swift")]
        Swift
    }
}

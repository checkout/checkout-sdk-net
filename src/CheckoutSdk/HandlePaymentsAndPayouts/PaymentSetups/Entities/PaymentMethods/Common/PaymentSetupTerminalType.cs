using System.Runtime.Serialization;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The client-side terminal type. Indicates whether the customer is using a PC browser, mobile
    /// browser, or mobile application.
    /// </summary>
    public enum PaymentSetupTerminalType
    {
        [EnumMember(Value = "web")]
        Web,

        [EnumMember(Value = "wap")]
        Wap,

        [EnumMember(Value = "app")]
        App
    }
}

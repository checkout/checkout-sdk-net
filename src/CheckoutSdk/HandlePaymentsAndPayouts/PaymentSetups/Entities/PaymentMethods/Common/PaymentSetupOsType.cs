using System.Runtime.Serialization;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The customer's operating system type. Required when terminal_type is not web.
    /// </summary>
    public enum PaymentSetupOsType
    {
        [EnumMember(Value = "android")]
        Android,

        [EnumMember(Value = "ios")]
        Ios
    }
}

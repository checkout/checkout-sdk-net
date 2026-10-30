using System.Runtime.Serialization;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The type of mandate.
    /// </summary>
    public enum PaymentSetupSepaMandateType
    {
        [EnumMember(Value = "core")]
        Core,

        [EnumMember(Value = "b2b")]
        B2B
    }
}

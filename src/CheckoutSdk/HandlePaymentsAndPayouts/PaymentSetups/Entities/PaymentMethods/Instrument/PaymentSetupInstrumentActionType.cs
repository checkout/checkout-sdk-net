using System.Runtime.Serialization;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The type of the next available action for the instrument payment method.
    /// </summary>
    public enum PaymentSetupInstrumentActionType
    {
        /// <summary>
        /// Set the instrument to use for the payment.
        /// </summary>
        [EnumMember(Value = "set_instrument")]
        SetInstrument
    }
}

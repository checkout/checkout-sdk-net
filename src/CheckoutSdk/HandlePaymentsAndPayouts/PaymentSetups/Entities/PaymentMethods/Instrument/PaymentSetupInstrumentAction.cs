namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The next available action for the instrument payment method (response only).
    /// </summary>
    public class PaymentSetupInstrumentAction
    {
        /// <summary>
        /// The type of action.
        /// [Optional]
        /// </summary>
        public PaymentSetupInstrumentActionType? Type { get; set; }
    }
}

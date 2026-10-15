namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The instrument payment method's details and configuration.
    /// </summary>
    public class PaymentSetupInstrument : PaymentSetupPaymentMethod
    {
        /// <summary>
        /// The unique identifier of the selected instrument.
        /// [Optional]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The type of the instrument, for example bacs.
        /// [Optional]
        /// readOnly
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The next available action for the payment method.
        /// [Optional]
        /// readOnly
        /// </summary>
        public PaymentSetupInstrumentAction Action { get; set; }
    }
}

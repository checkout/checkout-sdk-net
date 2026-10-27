namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// Base class for the payment methods that also carry an initialization state.
    /// </summary>
    public abstract class PaymentMethodBase : PaymentSetupPaymentMethod
    {
        /// <summary>
        /// The initialization state of the payment method. When you create a Payment Setup, this
        /// defaults to disabled. On update the API replaces the payment methods, so send "enabled" again
        /// to keep the method enabled. The specification does not state this; it is the API's behaviour.
        /// [Optional]
        /// Default: "disabled"
        /// </summary>
        public PaymentMethodInitialization Initialization { get; set; } = PaymentMethodInitialization.Disabled;
    }
}

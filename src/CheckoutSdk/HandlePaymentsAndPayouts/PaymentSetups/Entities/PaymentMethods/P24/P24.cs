namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The P24 (Przelewy24) payment method's details and configuration.
    /// </summary>
    public class P24 : PaymentSetupPaymentMethod
    {
        /// <summary>
        /// The account holder's details.
        /// [Optional]
        /// </summary>
        public P24AccountHolder AccountHolder { get; set; }
    }
}

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The Bancontact payment method's details and configuration.
    /// </summary>
    public class Bancontact : PaymentSetupPaymentMethod
    {
        /// <summary>
        /// The account holder's name.
        /// [Optional]
        /// min 3 characters, max 100 characters
        /// </summary>
        public string AccountHolderName { get; set; }
    }
}

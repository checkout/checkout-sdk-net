namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The Bancontact payment method's details and configuration.
    /// </summary>
    public class Bancontact : PaymentSetupPaymentMethod
    {
        /// <summary>
        /// The account holder's name.
        /// The specification marks this optional, but the API keeps the method invalid until it is sent.
        /// [Optional]
        /// min 3 characters, max 100 characters
        /// </summary>
        public string AccountHolderName { get; set; }
    }
}

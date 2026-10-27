namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The iDEAL payment method's details and configuration.
    /// </summary>
    public class Ideal : PaymentSetupPaymentMethod
    {
        /// <summary>
        /// A description of the products or services being paid for. Do not include HTML tags or special
        /// characters, as these are rejected by iDEAL.
        /// The specification marks this optional, but the API keeps the method invalid until it is sent.
        /// [Optional]
        /// max 35 characters
        /// </summary>
        public string Description { get; set; }
    }
}

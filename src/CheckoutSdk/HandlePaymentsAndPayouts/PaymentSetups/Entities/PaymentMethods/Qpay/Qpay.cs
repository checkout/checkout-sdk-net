namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The QPay payment method's details and configuration.
    /// </summary>
    public class Qpay : PaymentSetupPaymentMethod
    {
        /// <summary>
        /// The Qatari national ID. Must start with 2 or 3, followed by 10 digits.
        /// [Optional] writeOnly
        /// </summary>
        public string NationalId { get; set; }

        /// <summary>
        /// A description of the payment order. Alphanumeric characters only.
        /// [Optional]
        /// max 255 characters
        /// </summary>
        public string Description { get; set; }
    }
}

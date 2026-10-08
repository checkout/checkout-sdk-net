namespace Checkout.Issuing.Cards.Responses.Create
{
    /// <summary>
    /// The virtual card's credentials, returned when they were requested on creation.
    /// </summary>
    public class Credentials
    {
        /// <summary>
        /// The unencrypted card number.
        /// [Optional]
        /// Format: pan
        /// ^[0-9]{16}$
        /// 16 characters
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// The card's verification code, also known as the CVV or CVC2.
        /// [Optional]
        /// ^[0-9]{3}$
        /// 3 characters
        /// </summary>
        public string Cvc2 { get; set; }
    }
}

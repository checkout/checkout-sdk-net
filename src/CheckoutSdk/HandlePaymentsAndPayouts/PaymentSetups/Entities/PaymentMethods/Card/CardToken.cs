namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The Checkout.com card token to use for the card payment method.
    /// </summary>
    public class CardToken
    {
        /// <summary>
        /// The Checkout.com card token identifier.
        /// [Optional]
        /// </summary>
        public string Id { get; set; }
    }
}

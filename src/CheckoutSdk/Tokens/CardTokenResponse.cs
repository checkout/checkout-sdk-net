using Checkout.Common;

namespace Checkout.Tokens
{
    /// <summary>
    /// Defines the response following the successful tokenization of a card.
    /// </summary>
    public class CardTokenResponse : TokenResponse
    {
        /// <summary>
        /// Gets the cardholder's billing address.
        /// </summary>
        public Address BillingAddress { get; set; }

        /// <summary>
        /// Gets the cardholder's phone number.
        /// </summary>
        public Phone Phone { get; set; }

        /// <summary>
        /// Gets the cardholder's name.
        /// </summary>
        public string Name { get; set; }
    }
}
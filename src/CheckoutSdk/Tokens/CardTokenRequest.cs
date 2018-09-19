using Checkout.Common;
using Checkout.Payments;

namespace Checkout.Tokens
{
    public class CardTokenRequest : ITokenRequest
    {
        /// <summary>
        /// Exchange card details or a digital wallet payment token for a reference token that can be later used to request a card payment.
        /// To create tokens please authenticate using your public key
        /// </summary>
        /// <param name="number">The card number</param>
        /// <param name="expiryMonth">The two-digit expiry month of the card</param>
        /// <param name="expiryYear">The four-digit expiry year of the card</param>
        public CardTokenRequest(string number, int expiryMonth, int expiryYear)
        {
            Number = number;
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
        }

        /// <summary>
        /// The card number
        /// </summary>
        public string Number { get; }
        /// <summary>
        /// The two-digit expiry month of the card
        /// </summary>
        public int ExpiryMonth { get; }
        /// <summary>
        /// The four-digit expiry year of the card
        /// </summary>
        public int ExpiryYear { get; }
        /// <summary>
        /// The card-holder name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The card verification value/code. 3 digits, except for Amex (4 digits).
        /// </summary>
        public string Cvv { get; set; }
        /// <summary>
        /// The payment source owner's billing address
        /// </summary>
        public Address BillingAddress { get; set; }
        /// <summary>
        /// The payment source owner's phone number
        /// </summary>
        public Phone Phone { get; set; }

        public string Type => CardSource.TypeName;
    }
}
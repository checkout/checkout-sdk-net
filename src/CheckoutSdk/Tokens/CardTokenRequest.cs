using System;
using Checkout.Common;
using Checkout.Payments;

namespace Checkout.Tokens
{
    /// <summary>
    /// Defines a request to exchange card details for a reference token that can later be used to initiate a payment via a <see cref="TokenSource"/>.
    /// </summary>
    public class CardTokenRequest : ITokenRequest
    {
        /// <summary>
        /// Creates a new <see cref="CardTokenRequest"/> instance.
        /// </summary>
        /// <param name="number">The full card number.</param>
        /// <param name="expiryMonth">The two-digit expiry month of the card.</param>
        /// <param name="expiryYear">The four-digit expiry year of the card.</param>
        public CardTokenRequest(string number, int expiryMonth, int expiryYear)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("The card number is required.", nameof(number));
            
            if (expiryMonth < 1 || expiryMonth > 12)
                throw new ArgumentOutOfRangeException(nameof(expiryMonth), "The exiry month must be between 1 and 12");

            Number = number;
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
        }

        /// <summary>
        /// Gets the card number.
        /// </summary>
        public string Number { get; }
        
        /// <summary>
        /// Gets the two-digit expiry month of the card.
        /// </summary>
        public int ExpiryMonth { get; }
        
        /// <summary>
        /// Gets the four-digit expiry year of the card.
        /// </summary>
        public int ExpiryYear { get; }
        
        /// <summary>
        /// Gets or sets the cardholder name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the card verification value/code. 3 digits, except for Amex (4 digits).
        /// </summary>
        public string Cvv { get; set; }
        
        /// <summary>
        /// Gets or sets the cardholder's billing address.
        /// </summary>
        public Address BillingAddress { get; set; }
        
        /// <summary>
        /// Gets or sets the cardholder's phone number.
        /// </summary>
        public Phone Phone { get; set; }

        /// <summary>
        /// Gets the type of source.
        /// </summary>
        public string Type => CardSource.TypeName;
    }
}
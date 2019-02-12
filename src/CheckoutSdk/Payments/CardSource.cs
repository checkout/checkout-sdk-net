using System;
using Checkout.Common;

namespace Checkout.Payments
{
    /// <summary>
    /// Represents a full card source for a payment request (PCI compliant merchants only).
    /// </summary>
    public class CardSource : IRequestSource
    {
        public const string TypeName = "card";

        /// <summary>
        /// Creates a new <see cref="CardSource"/> instance with the specified card details.
        /// </summary>
        /// <param name="number">The card number.</param>
        /// <param name="expiryMonth">The two-digit expiry month of the card.</param>
        /// <param name="expiryYear">The four-digit expiry year of the card.</param>
        public CardSource(string number, int expiryMonth, int expiryYear)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("The card number is required.", nameof(number));

            if (expiryMonth < 1 || expiryMonth > 12)
                throw new ArgumentOutOfRangeException("The expiry month must be between 1 and 12", nameof(expiryMonth));
            
            Number = number;
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
        }

        /// <summary>
        /// Gets the full card number.
        /// </summary>
        public string Number { get; }
        
        /// <summary>
        /// Gets the expiry month of the card.
        /// </summary>
        public int ExpiryMonth { get; }
        
        /// <summary>
        /// Gets the four-digit expiry year of the card.
        /// </summary>
        public int ExpiryYear { get; }
        
        /// <summary>
        /// Gets the cardholder name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the card verification value/code. 3 digits, except for Amex (4 digits).
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
        public string Type => TypeName;
    }
}
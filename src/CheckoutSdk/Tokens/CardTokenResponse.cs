using Checkout.Common;
using Checkout.Payments;

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
        /// Gets the card's two-digit expiry month.
        /// </summary>
        public int ExpiryMonth { get; set; }
        
        /// <summary>
        /// Gets the card's four-digit expiry year.
        /// </summary>
        public int ExpiryYear { get; set; }
        
        /// <summary>
        /// Gets the cardholder's name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Get's the card scheme.
        /// </summary>
        public string Scheme { get; set; }
        
        /// <summary>
        /// Gets the last four digits of the card number.
        /// </summary>
        public string Last4 { get; set; }
        
        /// <summary>
        /// Gets the card issuer BIN.
        /// </summary>
        public string Bin { get; set; }
        
        /// <summary>
        /// Gets the card type.
        /// </summary>
        public string CardType { get; set; }
        
        /// <summary>
        /// Gets the card category.
        /// </summary>
        public string CardCategory { get; set; }
        
        /// <summary>
        /// Gets the name of the card issuer.
        /// </summary>
        public string Issuer { get; set; }
        
        /// <summary>
        /// Gets the card issuer country ISO-2 code.
        /// </summary>
        public string IssuerCountry { get; set; }
        
        /// <summary>
        /// Gets the issuer/card scheme product identifier.
        /// </summary>
        public string ProductId { get; set; }
        
        /// <summary>
        /// Gets the issuer/card scheme product type.
        /// </summary>
        public string ProductType { get; set; }
    }
}
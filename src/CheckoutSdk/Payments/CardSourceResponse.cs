using Checkout.Common;

namespace Checkout.Payments
{
    /// <summary>
    /// The card used to complete a payment request. 
    /// </summary>
    public class CardSourceResponse : IResponseSource
    {
        /// <summary>
        /// Gets the card source ID that can be used for subsequent payments via an <see cref="IdSource"/>.
        /// For new cards, this will only be provided if the payment was successful.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Gets the type of source.
        /// </summary>
        public string Type { get; set; }
        
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
        /// Gets the card scheme.
        /// </summary>
        /// <example>Visa</example>
        public string Scheme { get; set; }
        
        /// <summary>
        /// Gets the last four digits of the card number.
        /// </summary>
        public string Last4 { get; set; }
        
        /// <summary>
        /// Gets a value that uniquely identifies this particular card number. 
        /// You can use this to compare cards across customers.
        /// </summary>
        public string Fingerprint { get; set; }
        
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
        
        /// <summary>
        /// Gets the Address Verification System check result.
        /// </summary>
        public string AvsCheck { get; set; }
        
        /// <summary>
        /// Gets the CVV check result.
        /// </summary>
        public string CvvCheck { get; set; }
    }
}
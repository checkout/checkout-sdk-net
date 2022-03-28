using System;
using Checkout.Common;

namespace Checkout.Tokens
{
    /// <summary>
    /// Indicates successful token creation containing the token details.
    /// </summary>
    public class TokenResponse : Resource
    {
        /// <summary>
        /// Gets the token type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets the reference token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets the date/time the token will expire.
        /// </summary>
        public DateTime? ExpiresOn { get; set; }

        /// <summary>
        /// Gets the card's two-digit expiry month.
        /// </summary>
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// Gets the card's four-digit expiry year.
        /// </summary>
        public int ExpiryYear { get; set; }

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
        
        /// <summary>
        /// Gets the issuer/card scheme token format.
        /// </summary>
        public string TokenFormat { get; set; }


    }
}
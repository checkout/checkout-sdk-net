using System;
using Checkout.Common;

namespace Checkout.Tokens
{
    /// <summary>
    /// The token metadata response returned by GET /tokens/{tokenId}/metadata.
    /// </summary>
    public class TokenMetadataResponse : Resource
    {
        /// <summary>
        /// The token ID.
        /// [Required]
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The token type.
        /// [Required]
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The date/time the token will expire.
        /// [Required]
        /// Format: date-time (RFC 3339)
        /// </summary>
        public DateTime? ExpiresOn { get; set; }

        /// <summary>
        /// The card expiry month.
        /// [Required]
        /// &gt;= 1
        /// &lt;= 12
        /// </summary>
        public int? ExpiryMonth { get; set; }

        /// <summary>
        /// The card expiry year.
        /// [Required]
        /// </summary>
        public int? ExpiryYear { get; set; }

        /// <summary>
        /// The card scheme.
        /// [Optional]
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// The last four digits of the card number.
        /// [Required]
        /// </summary>
        public string Last4 { get; set; }

        /// <summary>
        /// The card issuer BIN.
        /// [Required]
        /// </summary>
        public string Bin { get; set; }

        /// <summary>
        /// The card type.
        /// [Optional]
        /// Enum: "CREDIT" "DEBIT" "PREPAID" "CHARGE" "DEFERRED DEBIT"
        /// </summary>
        public CardType? CardType { get; set; }

        /// <summary>
        /// The card category.
        /// [Optional]
        /// Enum: "CONSUMER" "COMMERCIAL"
        /// </summary>
        public CardCategory? CardCategory { get; set; }

        /// <summary>
        /// The name of the card issuer.
        /// [Optional]
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// The card issuer's country (two-letter ISO code).
        /// [Optional]
        /// 2 characters
        /// </summary>
        public string IssuerCountry { get; set; }

        /// <summary>
        /// The issuer or card scheme's product identifier.
        /// [Optional]
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// The issuer or card scheme's product type.
        /// [Optional]
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// Partial billing address — city and country only.
        /// [Optional]
        /// </summary>
        public TokenMetadataBillingAddress BillingAddress { get; set; }
    }

    /// <summary>
    /// Partial billing address — city and country only.
    /// </summary>
    public class TokenMetadataBillingAddress
    {
        /// <summary>
        /// The address city.
        /// [Optional]
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The billing address country's (two-letter ISO code).
        /// [Optional]
        /// 2 characters
        /// </summary>
        public string Country { get; set; }
    }
}

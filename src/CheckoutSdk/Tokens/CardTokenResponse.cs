using Checkout.Common;
using Checkout.Payments;

namespace Checkout.Tokens
{
    public class CardTokenResponse : TokenResponse
    {
        /// <summary>
        /// The payment source owner's billing address
        /// </summary>
        public Address BillingAddress { get; set; }
        /// <summary>
        /// The payment source owner's phone number
        /// </summary>
        public Phone Phone { get; set; }
        /// <summary>
        /// The two-digit expiry month
        /// </summary>
        public int ExpiryMonth { get; set; }
        /// <summary>
        /// The four-digit expiry year
        /// </summary>
        public int ExpiryYear { get; set; }
        /// <summary>
        /// The card-holder name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The card scheme
        /// </summary>
        public string Scheme { get; set; }
        /// <summary>
        /// The last four digits of the card number
        /// </summary>
        public string Last4 { get; set; }
        /// <summary>
        /// The card issuer BIN
        /// </summary>
        public string Bin { get; set; }
        /// <summary>
        /// The card type
        /// </summary>
        public CardType? CardType { get; set; }
        /// <summary>
        /// The card category
        /// </summary>
        public CardCategory? CardCategory { get; set; }
        /// <summary>
        /// The name of the card issuer
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// The card issuer country ISO-2 code
        /// </summary>
        public string IssuerCountry { get; set; }
        /// <summary>
        /// The issuer/card scheme product identifier
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// The issuer/card scheme product type
        /// </summary>
        public string ProductType { get; set; }
    }
}
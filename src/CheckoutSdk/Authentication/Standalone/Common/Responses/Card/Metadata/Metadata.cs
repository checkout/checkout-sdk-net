using Checkout.Common;

namespace Checkout.Authentication.Standalone.Common.Responses.Card.Metadata
{
    /// <summary>
    /// metadata
    /// Additional details for this card
    /// </summary>
    public class Metadata
    {
        /// <summary>
        /// The card type
        /// [Optional]
        /// </summary>
        public CardType? CardType { get; set; }

        /// <summary>
        /// The card category.
        /// [Optional]
        /// </summary>
        public CardCategoryType? CardCategory { get; set; }

        /// <summary>
        /// The card issuer's name.
        /// [Optional]
        /// </summary>
        public string IssuerName { get; set; }

        /// <summary>
        /// The two letter alpha country code of the card issuer.
        /// [Optional]
        /// </summary>
        public CountryCode? IssuerCountry { get; set; }

        /// <summary>
        /// The issuer/card scheme product identifier.
        /// [Optional]
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// The issuer/card scheme product type.
        /// [Optional]
        /// </summary>
        public string ProductType { get; set; }
    }
}
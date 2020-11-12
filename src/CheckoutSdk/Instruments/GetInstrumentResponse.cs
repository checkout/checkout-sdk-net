using Checkout.Common;

namespace Checkout.Instruments
{
    /// <summary>
    /// The instrument details.
    /// </summary>
    public class GetInstrumentResponse
    {
        /// <summary>
        /// The instrument id for the retrieved instrument.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The instrument type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// A token that can uniquely identify this card across all customers.
        /// </summary>
        public string Fingerprint { get; set; }

        /// <summary>
        /// Gets or sets the expiry month.
        /// </summary>
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// Gets or sets the expiry year.
        /// </summary>
        public int ExpiryYear { get; set; }

        /// <summary>
        /// Gets or sets the name of the cardholder.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the card scheme.
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// Gets or sets the last four digits of the card number.
        /// </summary>
        public string Last4 { get; set; }

        /// <summary>
        /// Gets or sets the card issuer's bank identification number (BIN).
        /// </summary>
        public string Bin { get; set; }

        /// <summary>
        /// Gets or sets the card type.
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// Gets or sets the card category.
        /// </summary>
        public string CardCategory { get; set; }

        /// <summary>
        /// Gets or sets the name of the card issuer.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the card issuer's country (two-letter ISO code).
        /// </summary>
        public string IssuerCountry { get; set; }

        /// <summary>
        /// Gets or sets the issuer/card scheme product identifier.
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// Gets or sets the issuer/card scheme product type.
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// Gets or sets the account holder details.
        /// </summary>
        public AccountHolder AccountHolder { get; set; }

        /// <summary>
        /// Gets or sets the customer details.
        /// </summary>
        public Customer Customer { get; set; }
    }
}

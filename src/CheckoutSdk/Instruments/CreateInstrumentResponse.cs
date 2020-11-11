namespace Checkout.Instruments
{
    /// <summary>
    /// Indicates the instrument has been successfully created.
    /// </summary>
    public class CreateInstrumentResponse
    {
        /// <summary>
        /// The unique identifier of the payment instrument that can be used later for payments.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The underlying payment instrument type.
        /// For payment instruments created from Checkout.com tokens, this will reflect the type of payment instrument that was tokenized.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the expiry month.
        /// </summary>
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// Gets or sets the expiry year.
        /// </summary>
        public int ExpiryYear { get; set; }

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
    }
}

using Checkout.Common;

namespace Checkout.Instruments.Create
{
    /// <summary>
    /// Store card instrument response.
    /// The type, id and fingerprint are inherited from CreateInstrumentResponse.
    /// </summary>
    public class CreateTokenInstrumentResponse : CreateInstrumentResponse
    {
        public CreateTokenInstrumentResponse() : base(InstrumentType.Card)
        {
        }

        /// <summary>
        /// The expiry month.
        /// [Required]
        /// min 1, max 2 characters
        /// </summary>
        public int? ExpiryMonth { get; set; }

        /// <summary>
        /// The expiry year.
        /// [Required]
        /// min 4 characters, max 4 characters
        /// </summary>
        public int? ExpiryYear { get; set; }

        /// <summary>
        /// The card scheme.
        /// [Optional]
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// The local co-branded card scheme.
        /// [Optional]
        /// Enum: "cartes_bancaires"
        /// </summary>
        public string SchemeLocal { get; set; }

        /// <summary>
        /// The last four digits of the card number.
        /// [Required]
        /// min 4 characters, max 4 characters
        /// </summary>
        public string Last4 { get; set; }

        /// <summary>
        /// The card issuer's bank identification number (BIN).
        /// [Required]
        /// </summary>
        public string Bin { get; set; }

        /// <summary>
        /// The card type.
        /// [Optional]
        /// </summary>
        public CardType? CardType { get; set; }

        /// <summary>
        /// The card category.
        /// [Optional]
        /// </summary>
        public CardCategory? CardCategory { get; set; }

        /// <summary>
        /// The name of the card issuer.
        /// [Optional]
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// The card issuer's country, as a two-letter ISO code.
        /// [Optional]
        /// min 2 characters, max 2 characters
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

        /// <summary>
        /// The account holder details.
        /// [Optional]
        /// </summary>
        public InstrumentAccountHolderResponse AccountHolder { get; set; }

        /// <summary>
        /// The customer's details.
        /// [Optional]
        /// </summary>
        public CustomerResponse Customer { get; set; }

        /// <summary>
        /// The network token details.
        /// [Optional]
        /// </summary>
        public InstrumentNetworkToken NetworkToken { get; set; }
    }
}

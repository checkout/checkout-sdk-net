using Checkout.Common;

namespace Checkout.Instruments.Get
{
    /// <summary>
    /// The details of a stored card instrument.
    /// </summary>
    public class GetCardInstrumentResponse : GetInstrumentResponse
    {
        public GetCardInstrumentResponse() : base(InstrumentType.Card)
        {
        }

        /// <summary>
        /// The JWE-encrypted full card number. This is only present if your level of PCI compliance
        /// is SAQ-D.
        /// [Optional]
        /// </summary>
        public string EncryptedCardNumber { get; set; }

        /// <summary>
        /// The expiry month.
        /// [Required]
        /// Minimum value 1. max 2 characters
        /// </summary>
        public int? ExpiryMonth { get; set; }

        /// <summary>
        /// The expiry year.
        /// [Required]
        /// min 4 characters, max 4 characters
        /// </summary>
        public int? ExpiryYear { get; set; }

        /// <summary>
        /// The name of the cardholder.
        /// [Optional]
        /// </summary>
        public string Name { get; set; }

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
        /// [Optional]
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
        /// The network token details.
        /// [Optional]
        /// </summary>
        public InstrumentNetworkToken NetworkToken { get; set; }

        /// <summary>
        /// The type of digital wallet used for the card.
        /// [Optional]
        /// </summary>
        public CardWalletType? CardWalletType { get; set; }

        /// <summary>
        /// Indicates whether the instrument is regulated debit.
        /// [Required]
        /// </summary>
        public bool? RegulatedIndicator { get; set; }
    }
}

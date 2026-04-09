using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Metadata.Card
{
    public class CardMetadataResponse : HttpMetadata
    {
        /// <summary>
        /// The issuer's Bank Identification Number (BIN).
        /// [Required] String, minLength: 6, maxLength: 11. Supports 6, 8, or 11 digit BINs.
        /// </summary>
        public string Bin { get; set; }

        /// <summary>
        /// The global card scheme. For example, <c>american_express</c>, <c>mastercard</c>, or <c>visa</c>.
        /// [Required]
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// The local card scheme, if the card is co-branded.
        /// [Optional] Replaced by <see cref="LocalSchemes"/>.
        /// </summary>
        [Obsolete("This property will be removed in the future, and should not be used. Use LocalSchemes instead.", false)]
        public SchemeLocalType? SchemeLocal { get; set; }

        /// <summary>
        /// The local card scheme or schemes, if the card is co-branded.
        /// [Optional] Values: <c>accel</c>, <c>cartes_bancaires</c>, <c>mada</c>, <c>nyce</c>,
        /// <c>omannet</c>, <c>pulse</c>, <c>shazam</c>, <c>star</c>, <c>upi</c>, <c>paypak</c>, <c>maestro</c>.
        /// </summary>
        public IList<SchemeLocalType> LocalSchemes { get; set; }

        /// <summary>
        /// The card type.
        /// [Optional] Values: <c>credit</c>, <c>debit</c>, <c>prepaid</c>, <c>charge</c>, <c>deferred_debit</c>.
        /// </summary>
        public CardMetadataType? CardType { get; set; }

        /// <summary>
        /// The card category.
        /// [Optional] Values: <c>consumer</c>, <c>commercial</c>.
        /// </summary>
        public CardCategory? CardCategory { get; set; }

        /// <summary>
        /// The card billing currency, as a three-letter ISO-4217 currency code.
        /// [Optional]
        /// </summary>
        public Currency? Currency { get; set; }

        /// <summary>
        /// The card issuer's name.
        /// [Optional]
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// The card issuer's country, as an ISO 3166-1 alpha-2 code.
        /// [Optional]
        /// </summary>
        public CountryCode? IssuerCountry { get; set; }

        /// <summary>
        /// The card issuer's country name.
        /// [Optional]
        /// </summary>
        public string IssuerCountryName { get; set; }

        /// <summary>
        /// Indicates whether the card is a combo credit and debit card. Applicable to Visa and Mastercard.
        /// [Optional]
        /// </summary>
        public bool? IsComboCard { get; set; }

        /// <summary>
        /// The card issuer or scheme's product identifier.
        /// [Optional]
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// The card issuer or scheme's product type.
        /// [Optional]
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// The card issuer or scheme's sub-product identifier.
        /// [Optional]
        /// </summary>
        public string SubproductId { get; set; }

        /// <summary>
        /// Specifies whether the card is assigned to an interchange regulated BIN range.
        /// [Optional]
        /// </summary>
        public bool? RegulatedIndicator { get; set; }

        /// <summary>
        /// The type of the interchange regulated card BIN range.
        /// [Optional] Values: <c>base_regulated</c>, <c>fraud_protected_regulated</c>.
        /// </summary>
        public string RegulatedType { get; set; }

        /// <summary>
        /// Indicates whether the prepaid bank identification number (BIN) is reloadable.
        /// [Optional]
        /// </summary>
        public bool? IsReloadablePrepaid { get; set; }

        /// <summary>
        /// The description of the prepaid BIN, specifying if it's an anonymous prepaid card.
        /// [Optional] Values: <c>Anonymous prepaid program and not AMLD5 compliant</c>,
        /// <c>Anonymous prepaid program and AMLD5 compliant</c>,
        /// <c>Not prepaid or non-anonymous prepaid program/default</c>.
        /// </summary>
        public string AnonymousPrepaidDescription { get; set; }

        /// <summary>
        /// Card payouts eligibility data. Present when the request format is <c>card_payouts</c>.
        /// [Optional]
        /// </summary>
        public CardMetadataPayouts CardPayouts { get; set; }

        /// <summary>
        /// Additional information about the scheme or local scheme's capabilities for PINless debit networks.
        /// Returned when the full card number or an 11-digit BIN is provided.
        /// [Optional]
        /// </summary>
        public SchemeMetadata SchemeMetadata { get; set; }

        /// <summary>
        /// Account Funding Transaction eligibility data.
        /// [Optional]
        /// </summary>
        public AccountFundingTransaction AccountFundingTransaction { get; set; }
    }
}

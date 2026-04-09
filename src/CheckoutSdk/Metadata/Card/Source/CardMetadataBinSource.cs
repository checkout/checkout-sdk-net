namespace Checkout.Metadata.Card.Source
{
    public class CardMetadataBinSource : CardMetadataRequestSource
    {
        public CardMetadataBinSource() : base(CardMetadataSourceType.Bin)
        {
        }

        /// <summary>
        /// The issuer's Bank Identification Number (BIN).
        /// [Required] String, minLength: 6, maxLength: 8. Pattern: numeric digits only.
        /// </summary>
        public string Bin { get; set; }
    }
}

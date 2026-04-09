namespace Checkout.Metadata.Card.Source
{
    public class CardMetadataCardSource : CardMetadataRequestSource
    {
        public CardMetadataCardSource() : base(CardMetadataSourceType.Card)
        {
        }

        /// <summary>
        /// The Primary Account Number (PAN).
        /// [Required] String, minLength: 12, maxLength: 19. Pattern: numeric digits only.
        /// </summary>
        public string Number { get; set; }
    }
}

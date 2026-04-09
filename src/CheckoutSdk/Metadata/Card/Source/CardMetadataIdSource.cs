namespace Checkout.Metadata.Card.Source
{
    public class CardMetadataIdSource : CardMetadataRequestSource
    {
        public CardMetadataIdSource() : base(CardMetadataSourceType.Id)
        {
        }

        /// <summary>
        /// The unique ID for the payment instrument created using the card's details.
        /// [Required] Pattern: <c>src_</c> followed by 26 alphanumeric characters.
        /// </summary>
        public string Id { get; set; }
    }
}

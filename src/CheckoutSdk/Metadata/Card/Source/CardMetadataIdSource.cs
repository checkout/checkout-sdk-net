namespace Checkout.Metadata.Card.Source
{
    public class CardMetadataIdSource : CardMetadataRequestSource
    {
        public CardMetadataIdSource() : base(CardMetadataSourceType.Id)
        {
        }

        public string Id { get; set; }
    }
}
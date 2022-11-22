namespace Checkout.Metadata.Card.Source
{
    public abstract class CardMetadataRequestSource
    {
        public CardMetadataSourceType? Type { get; set; }

        protected CardMetadataRequestSource(CardMetadataSourceType type)
        {
            Type = type;
        }
    }
}
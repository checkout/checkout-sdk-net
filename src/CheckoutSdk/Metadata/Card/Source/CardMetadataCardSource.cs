namespace Checkout.Metadata.Card.Source
{
    public class CardMetadataCardSource : CardMetadataRequestSource
    {
        public CardMetadataCardSource() : base(CardMetadataSourceType.Card)
        {
        }

        public string Number { get; set; }
    }
}
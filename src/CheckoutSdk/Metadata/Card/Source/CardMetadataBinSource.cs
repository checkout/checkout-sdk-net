namespace Checkout.Metadata.Card.Source
{
    public class CardMetadataBinSource : CardMetadataRequestSource
    {
        public CardMetadataBinSource() : base(CardMetadataSourceType.Bin)
        {
        }

        public string Bin { get; set; }
    }
}
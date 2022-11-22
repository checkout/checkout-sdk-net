namespace Checkout.Metadata.Card.Source
{
    public class CardMetadataTokenSource : CardMetadataRequestSource
    {
        public CardMetadataTokenSource() : base(CardMetadataSourceType.Token)
        {
        }

        public string Token { get; set; }
    }
}
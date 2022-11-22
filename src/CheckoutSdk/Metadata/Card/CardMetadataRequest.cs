using Checkout.Metadata.Card.Source;

namespace Checkout.Metadata.Card
{
    public class CardMetadataRequest
    {
        public CardMetadataRequestSource Source { get; set; }
        public CardMetadataFormatType? Format { get; set; }
    }
}
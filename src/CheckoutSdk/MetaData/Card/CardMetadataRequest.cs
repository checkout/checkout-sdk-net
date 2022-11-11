using Checkout.Metadata.Card.Types;

namespace Checkout.Metadata.Card
{
    public class CardMetadataRequest
    {
        public MetadataRequestSource Source { get; set; }
        public CardMetadataFormatType? Format { get; set; }
    }
}
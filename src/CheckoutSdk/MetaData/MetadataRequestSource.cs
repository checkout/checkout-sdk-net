using Checkout.Metadata.Card.Types;

namespace Checkout.Metadata
{
    public abstract class MetadataRequestSource
    {
        public CardMetadataSourceType? Type { get; set; }

        protected MetadataRequestSource(CardMetadataSourceType type)
        {
            Type = type;
        }
    }
}
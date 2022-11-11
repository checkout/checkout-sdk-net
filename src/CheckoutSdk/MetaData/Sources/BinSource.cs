using Checkout.Metadata.Card.Types;

namespace Checkout.Metadata.Sources
{
    public class BinSource : MetadataRequestSource
    {
        public BinSource() : base(CardMetadataSourceType.Bin)
        {
        }
        
        public string Bin { get; set; } 
    }
}
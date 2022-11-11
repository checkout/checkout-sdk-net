using Checkout.Metadata.Card.Types;

namespace Checkout.Metadata.Sources
{
    public class IdSource : MetadataRequestSource
    {
       public IdSource() : base(CardMetadataSourceType.Id)
        {
        }
       
       public string Id { get; set; }
    }
}
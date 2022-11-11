using Checkout.Metadata.Card.Types;

namespace Checkout.Metadata.Sources
{
    public class CardSource : MetadataRequestSource
    {
       public CardSource() : base(CardMetadataSourceType.Card)
        {
        }
       
       public string Number { get; set; }
    }
}
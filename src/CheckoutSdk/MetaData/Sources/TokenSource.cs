using Checkout.Metadata.Card.Types;

namespace Checkout.Metadata.Sources
{
    public class TokenSource : MetadataRequestSource
    {
       public TokenSource() : base(CardMetadataSourceType.Token)
        {
        }
       
       public string Id { get; set; }
    }
}
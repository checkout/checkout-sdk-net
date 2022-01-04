using Checkout.Common;
using System.Net.Mime;

namespace Checkout.Marketplace
{
    public class MarketplaceFileRequest : AbstractFileRequest
    {
         public MarketplaceFilePurpose Purpose { get; set; }

        public MarketplaceFileRequest(string file, ContentType contentType, MarketplaceFilePurpose purpose)
            : base(file, contentType)
        {
            this.Purpose = purpose;
        }
    }
}
using Checkout.Common;

namespace Checkout.Marketplace
{
    public class MarketplaceFileRequest : AbstractFileRequest
    {
        public MarketplaceFilePurpose Purpose { get; set; }
    }
}
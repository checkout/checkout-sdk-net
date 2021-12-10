using System.Collections.Generic;

namespace Checkout.Common
{
    public sealed class MarketplaceData
    {
        public string SubEntityId { get; set; }

        public IList<MarketplaceDataSubEntity> SubEntities { get; set; }
    }
}
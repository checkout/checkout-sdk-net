using System.Collections.Generic;

namespace Checkout.Common
{
    public class MarketplaceData
    {
        public string SubEntityId { get; set; }

        public IList<MarketplaceDataSubEntity> SubEntities { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace Checkout.Common
{
    public sealed class MarketplaceData : IEquatable<MarketplaceData>
    {
        public string SubEntityId { get; set; }

        public List<MarketplaceDataSubEntity> SubEntities { get; set; }

        public bool Equals(MarketplaceData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return SubEntityId == other.SubEntityId && Equals(SubEntities, other.SubEntities);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is MarketplaceData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SubEntityId, SubEntities);
        }
    }
}
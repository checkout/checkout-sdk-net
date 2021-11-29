using System;

namespace Checkout.Common
{
    public sealed class MarketplaceDataSubEntity : IEquatable<MarketplaceDataSubEntity>
    {
        public string Id { get; set; }

        public long? Amount { get; set; }

        public bool Equals(MarketplaceDataSubEntity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Amount == other.Amount;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is MarketplaceDataSubEntity other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Amount);
        }

        public class Comission
        {
            public long? Amount { get; set; }

            public double Percentage { get; set; }
        }
    }
}
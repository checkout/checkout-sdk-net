using System;

namespace Checkout.Payments.Request.Source.Apm
{
    public sealed class FawryProduct : IEquatable<FawryProduct>
    {
        public string ProductId { get; set; }

        public long? Quantity { get; set; }

        public long? Price { get; set; }

        public string Description { get; set; }

        public bool Equals(FawryProduct other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ProductId == other.ProductId && Quantity == other.Quantity && Price == other.Price &&
                   Description == other.Description;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is FawryProduct other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ProductId, Quantity, Price, Description);
        }
    }
}
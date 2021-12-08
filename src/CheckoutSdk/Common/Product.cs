using System;

namespace Checkout.Common
{
    public sealed class Product : IEquatable<Product>
    {
        public string Name { get; set; }
        public long Quantity { get; set; }
        public long Price { get; set; }

        public bool Equals(Product other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Quantity == other.Quantity && Price == other.Price;
        }
    }
}

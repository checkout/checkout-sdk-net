using System;

namespace Checkout.Common
{
    public sealed class Product : IEquatable<Product>
    {
        public string Name { get; set; }

        public long? Quantity { get; set; }

        public long? Price { get; set; }

        public bool Equals(Product other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name.Equals(other.Name) && Quantity.Equals(other.Quantity) 
                && Price.Equals(other.Price);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Product other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Quantity, Price);
        }
    }
}

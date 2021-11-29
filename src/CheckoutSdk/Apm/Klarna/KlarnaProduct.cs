using System;

namespace Checkout.Apm.Klarna
{
    public sealed class KlarnaProduct : IEquatable<KlarnaProduct>
    {
        public string Name { get; set; }

        public long? Quantity { get; set; }

        public long? UnitPrice { get; set; }

        public long? TaxRate { get; set; }

        public long? TotalAmount { get; set; }

        public long? TotalTaxAmount { get; set; }

        public bool Equals(KlarnaProduct other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Quantity == other.Quantity && UnitPrice == other.UnitPrice &&
                   TaxRate == other.TaxRate && TotalAmount == other.TotalAmount &&
                   TotalTaxAmount == other.TotalTaxAmount;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is KlarnaProduct other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Quantity, UnitPrice, TaxRate, TotalAmount, TotalTaxAmount);
        }
    }
}
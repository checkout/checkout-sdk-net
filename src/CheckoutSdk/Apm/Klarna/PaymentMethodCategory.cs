using System;

namespace Checkout.Apm.Klarna
{
    public sealed class PaymentMethodCategory : IEquatable<PaymentMethodCategory>
    {
        public string Identifier { get; set; }

        public string Name { get; set; }

        public PaymentMethodCategoryAssetUrl AssetUrls { get; set; }

        public bool Equals(PaymentMethodCategory other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Identifier == other.Identifier && Name == other.Name && Equals(AssetUrls, other.AssetUrls);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PaymentMethodCategory other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Identifier, Name, AssetUrls);
        }
    }
}
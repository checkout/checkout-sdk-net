using System;

namespace Checkout.Apm.Klarna
{
    public sealed class PaymentMethodCategoryAssetUrl : IEquatable<PaymentMethodCategoryAssetUrl>
    {
        public string Descriptive { get; set; }

        public string Standard { get; set; }

        public bool Equals(PaymentMethodCategoryAssetUrl other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Descriptive == other.Descriptive && Standard == other.Standard;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PaymentMethodCategoryAssetUrl other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Descriptive, Standard);
        }
    }
}
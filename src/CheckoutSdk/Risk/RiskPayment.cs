using System;

namespace Checkout.Risk
{
    public sealed class RiskPayment : IEquatable<RiskPayment>
    {
        public string Psp { get; set; }

        public string Id { get; set; }

        public bool Equals(RiskPayment other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Psp == other.Psp && Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is RiskPayment other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Psp, Id);
        }
    }
}
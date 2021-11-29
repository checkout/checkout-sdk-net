using System;

namespace Checkout.Payments
{
    public sealed class RiskAssessment : IEquatable<RiskAssessment>
    {
        public bool? Flagged { get; set; }

        public bool Equals(RiskAssessment other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Flagged == other.Flagged;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is RiskAssessment other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Flagged.GetHashCode();
        }
    }
}
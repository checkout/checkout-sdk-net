using System;

namespace Checkout.Sources
{
    public sealed class SourceData : IEquatable<SourceData>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AccountIban { get; set; }

        public string Bic { get; set; }

        public string BillingDescriptor { get; set; }

        public MandateType? MandateType { get; set; }

        public bool Equals(SourceData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return FirstName == other.FirstName && LastName == other.LastName && AccountIban == other.AccountIban &&
                   Bic == other.Bic && BillingDescriptor == other.BillingDescriptor && MandateType == other.MandateType;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is SourceData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName, AccountIban, Bic, BillingDescriptor, (int) MandateType);
        }
    }
}
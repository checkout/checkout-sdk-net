using System;
using Checkout.Common;

namespace Checkout.Sources
{
    public sealed class SepaSourceRequest : SourceRequest, IEquatable<SepaSourceRequest>
    {
        public SepaSourceRequest() : base(SourceType.Sepa)
        {
        }

        public Address BillingAddress { get; set; }

        public SourceData SourceData { get; set; }

        public bool Equals(SepaSourceRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(BillingAddress, other.BillingAddress) && Equals(SourceData, other.SourceData);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is SepaSourceRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BillingAddress, SourceData);
        }
    }
}
using System;
using Checkout.Common;

namespace Checkout.Payments.Request.Source
{
    public sealed class RequestIdSource : AbstractRequestSource, IEquatable<RequestIdSource>
    {
        public RequestIdSource() : base(PaymentSourceType.Id)
        {
        }

        public string Id { get; set; }

        public string Cvv { get; set; }

        public bool Equals(RequestIdSource other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Cvv == other.Cvv;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is RequestIdSource other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Cvv);
        }
    }
}
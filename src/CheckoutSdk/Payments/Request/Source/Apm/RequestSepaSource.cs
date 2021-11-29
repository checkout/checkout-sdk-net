using System;
using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public sealed class RequestSepaSource : AbstractRequestSource, IEquatable<RequestSepaSource>
    {
        public RequestSepaSource() : base(PaymentSourceType.Id)
        {
        }

        public string Id { get; set; }

        public bool Equals(RequestSepaSource other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is RequestSepaSource other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }
    }
}
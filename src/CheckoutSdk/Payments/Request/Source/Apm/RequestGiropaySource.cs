using System;
using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public sealed class RequestGiropaySource : AbstractRequestSource, IEquatable<RequestGiropaySource>
    {
        public RequestGiropaySource() : base(PaymentSourceType.Giropay)
        {
        }

        public string Purpose { get; set; }

        public bool Equals(RequestGiropaySource other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Purpose == other.Purpose;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is RequestGiropaySource other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Purpose != null ? Purpose.GetHashCode() : 0);
        }
    }
}
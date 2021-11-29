using System;
using Checkout.Common;

namespace Checkout.Payments.Request.Source
{
    public sealed class RequestCustomerSource : AbstractRequestSource, IEquatable<RequestCustomerSource>
    {
        public RequestCustomerSource() : base(PaymentSourceType.Customer)
        {
        }

        public string Id { get; set; }

        public bool Equals(RequestCustomerSource other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is RequestCustomerSource other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }
    }
}
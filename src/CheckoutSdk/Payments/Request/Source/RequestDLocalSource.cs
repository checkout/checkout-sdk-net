using System;
using Checkout.Common;

namespace Checkout.Payments.Request.Source
{
    public sealed class RequestDLocalSource : AbstractRequestSource, IEquatable<RequestDLocalSource>
    {
        public RequestDLocalSource() : base(PaymentSourceType.DLocal)
        {
        }

        public string Number { get; set; }

        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public string Name { get; set; }

        public string Cvv { get; set; }

        public bool? Stored { get; set; }

        public Address BillingAddress { get; set; }

        public Phone Phone { get; set; }

        public bool Equals(RequestDLocalSource other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Number == other.Number && ExpiryMonth == other.ExpiryMonth && ExpiryYear == other.ExpiryYear &&
                   Name == other.Name && Cvv == other.Cvv && Stored == other.Stored &&
                   Equals(BillingAddress, other.BillingAddress) && Equals(Phone, other.Phone);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is RequestDLocalSource other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Number, ExpiryMonth, ExpiryYear, Name, Cvv, Stored, BillingAddress, Phone);
        }
    }
}
using System;
using Checkout.Common;

namespace Checkout.Payments.Links
{
    public sealed class PaymentLinkResponse : Resource, IEquatable<PaymentLinkResponse>
    {
        public string Id { get; set; }

        public string PaymentId { get; set; }

        public DateTime? ExpiresOn { get; set; }

        public string Reference { get; set; }

        public bool Equals(PaymentLinkResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id) && ExpiresOn.Equals(other.ExpiresOn) && Reference.Equals(other.Reference);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PaymentLinkResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, ExpiresOn, Reference);
        }
    }
}
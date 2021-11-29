using System;
using Checkout.Common;

namespace Checkout.Payments.Four
{
    public sealed class VoidResponse : Resource, IEquatable<VoidResponse>
    {
        public string ActionId { get; set; }

        public string Reference { get; set; }

        public bool Equals(VoidResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ActionId == other.ActionId && Reference == other.Reference;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is VoidResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ActionId, Reference);
        }
    }
}
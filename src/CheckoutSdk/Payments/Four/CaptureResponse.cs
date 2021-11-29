using System;
using Checkout.Common;

namespace Checkout.Payments.Four
{
    public sealed class CaptureResponse : Resource, IEquatable<CaptureResponse>
    {
        public string ActionId { get; set; }

        public string Reference { get; set; }

        public bool Equals(CaptureResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ActionId == other.ActionId && Reference == other.Reference;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is CaptureResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ActionId, Reference);
        }
    }
}
using System;

namespace Checkout.Common
{
    public sealed class IdResponse : Resource, IEquatable<IdResponse>
    {
        public string Id { get; set; }

        public bool Equals(IdResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is IdResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }
    }
}
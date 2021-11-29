using System;

namespace Checkout.Common.Four
{
    public sealed class UpdateCustomerRequest : IEquatable<UpdateCustomerRequest>
    {
        public string Id { get; set; }

        public bool? Default { get; set; }

        public bool Equals(UpdateCustomerRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Default == other.Default;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is UpdateCustomerRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Default);
        }
    }
}
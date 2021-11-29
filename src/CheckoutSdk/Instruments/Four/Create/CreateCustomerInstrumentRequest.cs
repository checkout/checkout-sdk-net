using System;
using Checkout.Common;

namespace Checkout.Instruments.Four.Create
{
    public sealed class CreateCustomerInstrumentRequest : IEquatable<CreateCustomerInstrumentRequest>
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public Phone Phone { get; set; }

        public bool Default { get; set; }

        public bool Equals(CreateCustomerInstrumentRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Email == other.Email && Name == other.Name && Equals(Phone, other.Phone) &&
                   Default == other.Default;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is CreateCustomerInstrumentRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Email, Name, Phone, Default);
        }
    }
}
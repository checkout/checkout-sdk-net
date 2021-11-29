using System;

namespace Checkout.Instruments
{
    public sealed class UpdateInstrumentRequest : IEquatable<UpdateInstrumentRequest>
    {
        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public string Name { get; set; }

        public InstrumentAccountHolder AccountHolder { get; set; }

        public UpdateInstrumentCustomer Customer { get; set; }

        public bool Equals(UpdateInstrumentRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ExpiryMonth == other.ExpiryMonth && ExpiryYear == other.ExpiryYear && Name == other.Name &&
                   Equals(AccountHolder, other.AccountHolder) && Equals(Customer, other.Customer);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is UpdateInstrumentRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ExpiryMonth, ExpiryYear, Name, AccountHolder, Customer);
        }

        public sealed class UpdateInstrumentCustomer : IEquatable<UpdateInstrumentCustomer>
        {
            public string Id { get; set; }

            public bool Default { get; set; }

            public bool Equals(UpdateInstrumentCustomer other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Id == other.Id && Default == other.Default;
            }

            public override bool Equals(object obj)
            {
                return ReferenceEquals(this, obj) || obj is UpdateInstrumentCustomer other && Equals(other);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Id, Default);
            }
        }
    }
}
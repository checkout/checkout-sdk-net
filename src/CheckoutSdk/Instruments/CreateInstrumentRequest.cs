using System;

namespace Checkout.Instruments
{
    public sealed class CreateInstrumentRequest : IEquatable<CreateInstrumentRequest>
    {
        public InstrumentType Type => InstrumentType.Token;

        public string Token { get; set; }

        public InstrumentAccountHolder AccountHolder { get; set; }

        public InstrumentCustomerRequest Customer { get; set; }

        public bool Equals(CreateInstrumentRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Token == other.Token && Equals(AccountHolder, other.AccountHolder) &&
                   Equals(Customer, other.Customer);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is CreateInstrumentRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Token, AccountHolder, Customer);
        }
    }
}
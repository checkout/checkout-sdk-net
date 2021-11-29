using System;
using Checkout.Common.Four;

namespace Checkout.Instruments.Four.Create
{
    public sealed class CreateTokenInstrumentRequest : CreateInstrumentRequest, IEquatable<CreateTokenInstrumentRequest>
    {
        public CreateTokenInstrumentRequest() : base(InstrumentType.Token)
        {
        }

        public string Token { get; set; }

        public AccountHolder AccountHolder { get; set; }

        public CreateCustomerInstrumentRequest Customer { get; set; }

        public bool Equals(CreateTokenInstrumentRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Token == other.Token && Equals(AccountHolder, other.AccountHolder) &&
                   Equals(Customer, other.Customer);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is CreateTokenInstrumentRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Token, AccountHolder, Customer);
        }
    }
}
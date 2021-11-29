using System;
using Checkout.Common.Four;

namespace Checkout.Instruments.Four.Update
{
    public sealed class UpdateCardInstrumentRequest : UpdateInstrumentRequest, IEquatable<UpdateCardInstrumentRequest>
    {
        public UpdateCardInstrumentRequest() : base(InstrumentType.Card)
        {
        }

        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public string Name { get; set; }

        public UpdateCustomerRequest Customer { get; set; }

        public AccountHolder AccountHolder { get; set; }

        public bool Equals(UpdateCardInstrumentRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ExpiryMonth == other.ExpiryMonth && ExpiryYear == other.ExpiryYear && Name == other.Name &&
                   Equals(Customer, other.Customer) && Equals(AccountHolder, other.AccountHolder);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is UpdateCardInstrumentRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ExpiryMonth, ExpiryYear, Name, Customer, AccountHolder);
        }
    }
}
using System;
using Checkout.Common;
using Checkout.Common.Four;

namespace Checkout.Instruments.Four.Create
{
    public sealed class CreateBankAccountInstrumentResponse : CreateInstrumentResponse,
        IEquatable<CreateBankAccountInstrumentResponse>
    {
        public CreateBankAccountInstrumentResponse() : base(InstrumentType.BankAccount)
        {
        }

        public string Fingerprint { get; set; }

        public CustomerResponse Customer { get; set; }

        public BankDetails Bank { get; set; }

        public string SwiftBic { get; set; }

        public string AccountNumber { get; set; }

        public string BankCode { get; set; }

        public string Iban { get; set; }

        public bool Equals(CreateBankAccountInstrumentResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Fingerprint == other.Fingerprint && Equals(Customer, other.Customer) && Equals(Bank, other.Bank) &&
                   SwiftBic == other.SwiftBic && AccountNumber == other.AccountNumber && BankCode == other.BankCode &&
                   Iban == other.Iban;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is CreateBankAccountInstrumentResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Fingerprint, Customer, Bank, SwiftBic, AccountNumber, BankCode, Iban);
        }
    }
}
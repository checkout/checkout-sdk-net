using System;
using Checkout.Common;
using Checkout.Common.Four;

namespace Checkout.Instruments.Four.Get
{
    public sealed class GetBankAccountInstrumentResponse : GetInstrumentResponse,
        IEquatable<GetBankAccountInstrumentResponse>
    {
        public GetBankAccountInstrumentResponse() : base(InstrumentType.BankAccount)
        {
        }

        public AccountType? AccountType { get; set; }

        public string AccountNumber { get; set; }

        public string BankCode { get; set; }

        public string BranchCode { get; set; }

        public string Iban { get; set; }

        public string Bban { get; set; }

        public string SwiftBic { get; set; }

        public Currency? Currency { get; set; }

        public CountryCode? Country { get; set; }

        public BankDetails BankDetails { get; set; }

        public bool Equals(GetBankAccountInstrumentResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return AccountType == other.AccountType && AccountNumber == other.AccountNumber &&
                   BankCode == other.BankCode && BranchCode == other.BranchCode && Iban == other.Iban &&
                   Bban == other.Bban && SwiftBic == other.SwiftBic && Currency == other.Currency &&
                   Country == other.Country && Equals(BankDetails, other.BankDetails);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is GetBankAccountInstrumentResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add((int) AccountType);
            hashCode.Add(AccountNumber);
            hashCode.Add(BankCode);
            hashCode.Add(BranchCode);
            hashCode.Add(Iban);
            hashCode.Add(Bban);
            hashCode.Add(SwiftBic);
            hashCode.Add((int) Currency);
            hashCode.Add((int) Country);
            hashCode.Add(BankDetails);
            return hashCode.ToHashCode();
        }
    }
}
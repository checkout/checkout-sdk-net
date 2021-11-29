using System;
using Checkout.Common;
using Checkout.Common.Four;

namespace Checkout.Instruments.Four.Update
{
    public sealed class UpdateBankInstrumentRequest : UpdateInstrumentRequest, IEquatable<UpdateBankInstrumentRequest>
    {
        public UpdateBankInstrumentRequest() : base(InstrumentType.BankAccount)
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

        public string ProcessingChannelId { get; set; }

        public AccountHolder AccountHolder { get; set; }

        public BankDetails BankDetails { get; set; }

        public UpdateCustomerRequest Customer { get; set; }

        public bool Equals(UpdateBankInstrumentRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return AccountType == other.AccountType && AccountNumber == other.AccountNumber &&
                   BankCode == other.BankCode && BranchCode == other.BranchCode && Iban == other.Iban &&
                   Bban == other.Bban && SwiftBic == other.SwiftBic && Currency == other.Currency &&
                   Country == other.Country && ProcessingChannelId == other.ProcessingChannelId &&
                   Equals(AccountHolder, other.AccountHolder) && Equals(BankDetails, other.BankDetails) &&
                   Equals(Customer, other.Customer);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is UpdateBankInstrumentRequest other && Equals(other);
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
            hashCode.Add(ProcessingChannelId);
            hashCode.Add(AccountHolder);
            hashCode.Add(BankDetails);
            hashCode.Add(Customer);
            return hashCode.ToHashCode();
        }
    }
}
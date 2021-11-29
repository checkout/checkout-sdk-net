using System;
using Checkout.Common;
using Checkout.Common.Four;

namespace Checkout.Payments.Four.Request.Destination
{
    public sealed class PaymentBankAccountDestination : PaymentRequestDestination,
        IEquatable<PaymentBankAccountDestination>
    {
        public PaymentBankAccountDestination() : base(PaymentDestinationType.BankAccount)
        {
        }

        public AccountType? AccountType { get; set; }

        public string AccountNumber { get; set; }

        public string BankCode { get; set; }

        public string Iban { get; set; }

        public string Bban { get; set; }

        public string SwiftBic { get; set; }

        public CountryCode? Country { get; set; }

        public AccountHolder AccountHolder { get; set; }

        public BankDetails Bank { get; set; }

        public bool Equals(PaymentBankAccountDestination other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return AccountType == other.AccountType && AccountNumber == other.AccountNumber &&
                   BankCode == other.BankCode && Iban == other.Iban && Bban == other.Bban &&
                   SwiftBic == other.SwiftBic && Country == other.Country &&
                   Equals(AccountHolder, other.AccountHolder) && Equals(Bank, other.Bank);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PaymentBankAccountDestination other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add((int) AccountType);
            hashCode.Add(AccountNumber);
            hashCode.Add(BankCode);
            hashCode.Add(Iban);
            hashCode.Add(Bban);
            hashCode.Add(SwiftBic);
            hashCode.Add(Country);
            hashCode.Add(AccountHolder);
            hashCode.Add(Bank);
            return hashCode.ToHashCode();
        }
    }
}
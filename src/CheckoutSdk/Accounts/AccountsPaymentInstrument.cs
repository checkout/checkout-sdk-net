using Checkout.Common;
using Checkout.Common.Four;
using Checkout.Instruments;

namespace Checkout.Accounts
{
    public class AccountsPaymentInstrument
    {
        public readonly InstrumentType Type = InstrumentType.BankAccount;

        public string Label { get; set; }

        public AccountType? AccountType { get; set; }

        public string AccountNumber { get; set; }

        public string BankCode { get; set; }

        public string BranchCode { get; set; }

        public string Iban { get; set; }

        public string Bban { get; set; }

        public string SwiftBic { get; set; }

        public Currency? Currency { get; set; }

        public CountryCode? Country { get; set; }

        public InstrumentDocument Document { get; set; }

        public BankDetails Bank { get; set; }

        public AccountsAccountHolder AccountHolder { get; set; }
    }
}
using Checkout.Common;

namespace Checkout.Instruments.Get
{
    public class GetBankAccountInstrumentResponse : GetInstrumentResponse
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
    }
}
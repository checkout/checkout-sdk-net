using Checkout.Common;
using Checkout.Common.Four;

namespace Checkout.Instruments.Four.Update
{
    public sealed class UpdateBankInstrumentRequest : UpdateInstrumentRequest
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
    }
}
using Checkout.Common;

namespace Checkout.Instruments.Create
{
    public class CreateBankAccountInstrumentRequest : CreateInstrumentRequest
    {
        public CreateBankAccountInstrumentRequest() : base(InstrumentType.BankAccount)
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

        public BankDetails Bank{ get; set; }

        public CreateCustomerInstrumentRequest Customer { get; set; }
    }
}
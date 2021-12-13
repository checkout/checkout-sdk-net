using Checkout.Common;
using Checkout.Common.Four;

namespace Checkout.Instruments.Four.Create
{
    public sealed class CreateBankAccountInstrumentResponse : CreateInstrumentResponse
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
    }
}
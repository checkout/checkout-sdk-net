using Checkout.Common;

namespace Checkout.Instruments.Create
{
    public class CreateSepaInstrumentRequest : CreateInstrumentRequest
    {
        public CreateSepaInstrumentRequest() : base(InstrumentType.Sepa)
        {
        }

        public InstrumentData InstrumentData { get; set; }

        public AccountHolder AccountHolder { get; set; }
    }
}
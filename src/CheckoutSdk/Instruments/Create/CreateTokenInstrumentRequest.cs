using Checkout.Common;

namespace Checkout.Instruments.Create
{
    public class CreateTokenInstrumentRequest : CreateInstrumentRequest
    {
        public CreateTokenInstrumentRequest() : base(InstrumentType.Token)
        {
        }

        public string Token { get; set; }

        public AccountHolder AccountHolder { get; set; }

        public CreateCustomerInstrumentRequest Customer { get; set; }
    }
}
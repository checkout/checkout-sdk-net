using Checkout.Common.Four;

namespace Checkout.Instruments.Four.Create
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
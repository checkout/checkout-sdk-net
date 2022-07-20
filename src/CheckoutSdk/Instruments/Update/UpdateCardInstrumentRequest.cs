using Checkout.Common;

namespace Checkout.Instruments.Update
{
    public class UpdateCardInstrumentRequest : UpdateInstrumentRequest
    {
        public UpdateCardInstrumentRequest() : base(InstrumentType.Card)
        {
        }

        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public string Name { get; set; }

        public UpdateCustomerRequest Customer { get; set; }

        public AccountHolder AccountHolder { get; set; }
    }
}
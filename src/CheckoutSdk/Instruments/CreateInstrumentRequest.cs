namespace Checkout.Instruments
{
    public class CreateInstrumentRequest
    {
        public InstrumentType Type => InstrumentType.Token;

        public string Token { get; set; }

        public InstrumentAccountHolder AccountHolder { get; set; }

        public InstrumentCustomerRequest Customer { get; set; }
    }
}
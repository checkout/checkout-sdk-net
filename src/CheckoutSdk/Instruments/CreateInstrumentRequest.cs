namespace Checkout.Instruments
{
    public sealed class CreateInstrumentRequest
    {
        public InstrumentType Type => InstrumentType.Token;

        public string Token { get; set; }

        public InstrumentAccountHolder AccountHolder { get; set; }

        public InstrumentCustomerRequest Customer { get; set; }
    }
}
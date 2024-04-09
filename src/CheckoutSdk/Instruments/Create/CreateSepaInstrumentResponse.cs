namespace Checkout.Instruments.Create
{
    public class CreateSepaInstrumentResponse : CreateInstrumentResponse
    {
        public CreateSepaInstrumentResponse() : base(InstrumentType.Sepa)
        {
        }
    }
}
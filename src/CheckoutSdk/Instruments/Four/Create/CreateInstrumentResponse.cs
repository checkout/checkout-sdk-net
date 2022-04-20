namespace Checkout.Instruments.Four.Create
{
    public class CreateInstrumentResponse
    {
        public InstrumentType? Type { get; set; }

        public string Id { get; set; }

        public CreateInstrumentResponse(InstrumentType type)
        {
            Type = type;
        }
    }
}
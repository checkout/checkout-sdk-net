namespace Checkout.Instruments.Four.Create
{
    public abstract class CreateInstrumentResponse
    {
        public InstrumentType? Type { get; set; }

        public string Id { get; set; }

        protected CreateInstrumentResponse(InstrumentType type)
        {
            Type = type;
        }
    }
}
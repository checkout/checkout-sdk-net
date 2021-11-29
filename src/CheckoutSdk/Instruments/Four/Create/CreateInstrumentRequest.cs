namespace Checkout.Instruments.Four.Create
{
    public abstract class CreateInstrumentRequest
    {
        public InstrumentType? Type { get; set; }

        protected CreateInstrumentRequest(InstrumentType type)
        {
            Type = type;
        }
    }
}
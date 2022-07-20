namespace Checkout.Instruments.Update
{
    public abstract class UpdateInstrumentRequest
    {
        public InstrumentType? Type { get; set; }

        protected UpdateInstrumentRequest(InstrumentType type)
        {
            Type = type;
        }
    }
}
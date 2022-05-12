namespace Checkout.Instruments.Four.Update
{
    public class UpdateInstrumentResponse
    {
        public InstrumentType? Type { get; set; }

        public string Id { get; set; }

        public UpdateInstrumentResponse(InstrumentType? type)
        {
            Type = type;
        }
    }
}
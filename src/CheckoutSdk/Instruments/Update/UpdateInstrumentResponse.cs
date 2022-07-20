namespace Checkout.Instruments.Update
{
    public class UpdateInstrumentResponse : HttpMetadata
    {
        public InstrumentType? Type { get; set; }

        public string Id { get; set; }

        public UpdateInstrumentResponse(InstrumentType? type)
        {
            Type = type;
        }
    }
}
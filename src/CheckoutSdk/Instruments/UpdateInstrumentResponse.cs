namespace Checkout.Instruments
{
    public class UpdateInstrumentResponse
    {
        public InstrumentType? Type { get; set; }

        public string Fingerprint { get; set; }
    }
}
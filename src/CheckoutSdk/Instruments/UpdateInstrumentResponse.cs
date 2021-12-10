namespace Checkout.Instruments
{
    public sealed class UpdateInstrumentResponse
    {
        public InstrumentType? Type { get; set; }

        public string Fingerprint { get; set; }
    }
}
namespace Checkout.Marketplace
{
    public class Instrument
    {
        public string Id { get; set; }

        public string Label { get; set; }

        public InstrumentStatus? Status { get; set; }

        public InstrumentDocument Document { get; set; }
    }
}
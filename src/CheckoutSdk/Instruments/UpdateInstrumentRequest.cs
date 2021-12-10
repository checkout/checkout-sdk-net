namespace Checkout.Instruments
{
    public sealed class UpdateInstrumentRequest
    {
        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public string Name { get; set; }

        public InstrumentAccountHolder AccountHolder { get; set; }

        public UpdateInstrumentCustomer Customer { get; set; }

        public sealed class UpdateInstrumentCustomer
        {
            public string Id { get; set; }

            public bool Default { get; set; }
        }
    }
}
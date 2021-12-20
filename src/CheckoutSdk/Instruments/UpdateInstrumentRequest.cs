namespace Checkout.Instruments
{
    public class UpdateInstrumentRequest
    {
        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public string Name { get; set; }

        public InstrumentAccountHolder AccountHolder { get; set; }

        public UpdateInstrumentCustomer Customer { get; set; }

        public class UpdateInstrumentCustomer
        {
            public string Id { get; set; }

            public bool Default { get; set; }
        }
    }
}
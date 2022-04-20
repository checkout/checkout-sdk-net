using Checkout.Common;

namespace Checkout.Instruments.Four.Create
{
    public abstract class CreateInstrumentResponse
    {
        public InstrumentType? Type { get; set; }

        public string Id { get; set; }
        
        public string Fingerprint { get; set; }
        
        public CustomerResponse Customer { get; set; }

        protected CreateInstrumentResponse(InstrumentType type)
        {
            Type = type;
        }
    }
}
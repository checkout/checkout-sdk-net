using Checkout.Common;

namespace Checkout.Instruments.Create
{
    public class CreateInstrumentResponse : HttpMetadata
    {
        public InstrumentType? Type { get; set; }

        public string Id { get; set; }

        public string Fingerprint { get; set; }

        public CustomerResponse Customer { get; set; }

        public CreateInstrumentResponse(InstrumentType? type)
        {
            Type = type;
        }
    }
}
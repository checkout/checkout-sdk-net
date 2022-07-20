using Checkout.Common;

namespace Checkout.Instruments.Get
{
    public class GetInstrumentResponse : HttpMetadata
    {
        public GetInstrumentResponse()
        {
        }

        public GetInstrumentResponse(InstrumentType? type)
        {
            Type = type;
        }

        public InstrumentType? Type { get; set; }

        public string Id { get; set; }

        public string Fingerprint { get; set; }

        public InstrumentCustomerResponse Customer { get; set; }

        public AccountHolder AccountHolder { get; set; }
    }
}
using Checkout.Common.Four;

namespace Checkout.Instruments.Four.Get
{
    public class GetInstrumentResponse
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
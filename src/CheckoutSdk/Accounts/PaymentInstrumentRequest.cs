using Checkout.Common;
using Checkout.Instruments;
using Newtonsoft.Json;

namespace Checkout.Accounts
{
    public class PaymentInstrumentRequest
    {
        public string Label { get; set; }

        public InstrumentType? Type { get; set; }

        public Currency? Currency { get; set; }

        public CountryCode? Country { get; set; }

        [JsonProperty(PropertyName = "default")]
        public bool? DefaultDestination { get; set; }

        public InstrumentDocument Document { get; set; }

        public IInstrumentDetails InstrumentDetails { get; set; }
    }
}
using Checkout.Common;
using Checkout.Instruments;
using Newtonsoft.Json;

namespace Checkout.Accounts
{
    public class PaymentInstrumentDetailsResponse : Resource
    {
        public string Id { get; set; }

        public InstrumentStatus? Status { get; set; }

        public string InstrumentId { get; set; }

        public string Label { get; set; }

        public InstrumentType? Type { get; set; }

        public Currency? Currency { get; set; }

        public CountryCode? Country { get; set; }

        [JsonProperty(PropertyName = "default")]
        public bool? DefaultDestination { get; set; }

        public InstrumentDocument Document { get; set; }
    }
}
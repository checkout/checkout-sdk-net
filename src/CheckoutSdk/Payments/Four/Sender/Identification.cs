using Newtonsoft.Json;

namespace Checkout.Payments.Four.Sender
{
    public class Identification
    {
        public IdentificationType Type { get; set; }
        public string Number { get; set; }

        [JsonProperty("issuing_country")]
        public string IssuingCountry { get; set; }
    }
}
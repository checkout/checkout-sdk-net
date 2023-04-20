using Newtonsoft.Json;

namespace Checkout.Accounts
{
    public class UpdatePaymentInstrumentRequest
    {
        public string Label { get; set; }

        [JsonProperty(PropertyName = "default")]
        public bool? DefaultDestination { get; set; }
        
        public Headers Headers { get; set; }
    }
}
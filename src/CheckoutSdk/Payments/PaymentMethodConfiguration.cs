using Newtonsoft.Json;
using System.Collections.Generic;

namespace Checkout.Payments
{
    public class PaymentMethodConfiguration
    {

        [JsonProperty(PropertyName = "applepay")]
        public Applepay Applepay { get; set; }

        public Card Card { get; set; }

        [JsonProperty(PropertyName = "googlepay")]
        public Googlepay Googlepay { get; set; }

        public StoredCardConfiguration StoredCard { get; set; }
    }
}
using Checkout.Common;
using Checkout.Instruments;
#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif
using System.Collections.Generic;

namespace Checkout.Customers
{
    public class CustomerDetailsResponse
    {
        public string Id { get; set; }

        public string Email { get; set; }

#if NET5_0_OR_GREATER
        [JsonPropertyName("default")]
#else
        [JsonProperty(PropertyName = "default")]
#endif
        public string DefaultId { get; set; }

        public string Name { get; set; }

        public Phone Phone { get; set; }

        public IDictionary<string, object> Metadata { get; set; }

        public IList<InstrumentDetails> Instruments { get; set; }
    }
}
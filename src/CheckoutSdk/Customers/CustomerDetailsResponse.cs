using Checkout.Common;
using Checkout.Instruments;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Checkout.Customers
{
    public class CustomerDetailsResponse : HttpMetadata
    {
        public string Id { get; set; }

        public string Email { get; set; }

        [JsonProperty(PropertyName = "default")]
        public string DefaultId { get; set; }

        public string Name { get; set; }

        public Phone Phone { get; set; }

        public IDictionary<string, object> Metadata { get; set; }

        public IList<InstrumentDetails> Instruments { get; set; }
    }
}
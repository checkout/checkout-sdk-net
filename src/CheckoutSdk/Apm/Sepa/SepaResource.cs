using Checkout.Common;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Checkout.Apm.Sepa
{
    public class SepaResource : HttpMetadata
    {
        public SepaResource()
        {
            Links = new Dictionary<string, Link>();
        }

        [JsonProperty(PropertyName = "_links")]
        public IDictionary<string, Link> Links { get; set; }
    }
}
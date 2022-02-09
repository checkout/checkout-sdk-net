using Checkout.Common;
#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif
using System.Collections.Generic;

namespace Checkout.Apm.Sepa
{
    public class SepaResource
    {
        public SepaResource()
        {
            Links = new Dictionary<string, Link>();
        }


#if NET5_0_OR_GREATER
        [JsonPropertyName("_links")]
#else
        [JsonProperty(PropertyName = "_links")]
#endif
        public IDictionary<string, Link> Links { get; set; }
    }
}
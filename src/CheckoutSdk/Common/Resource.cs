#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif
using System.Collections.Generic;

namespace Checkout.Common
{
    public class Resource
    {
        public Resource()
        {
            Links = new Dictionary<string, Link>();
        }

#if NET5_0_OR_GREATER
        [JsonPropertyName("_links")]
#else
        [JsonProperty(PropertyName = "_links")]
#endif  
        public IDictionary<string, Link> Links { get; set; }

        public Link GetSelfLink()
        {
            return GetLink("self");
        }

        public bool HasLink(string relation)
        {
            return Links.ContainsKey(relation);
        }

        public Link GetLink(string relation)
        {
            Links.TryGetValue(relation, out var link);
            return link;
        }
    }
}
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Checkout
{
    public class Resource
    {
        public Resource()
        {
            Links = new Dictionary<string, Link>();
        }
        
        [JsonProperty(PropertyName = "_links")]
        public Dictionary<string, Link> Links { get; set; }

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
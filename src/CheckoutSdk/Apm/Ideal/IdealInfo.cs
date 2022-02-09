#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif

namespace Checkout.Apm.Ideal
{
    public class IdealInfo
    {

#if NET5_0_OR_GREATER
        [JsonPropertyName("_links")]
#else
        [JsonProperty(PropertyName = "_links")]
#endif
        public IdealInfoLinks Links { get; set; }
    }
}
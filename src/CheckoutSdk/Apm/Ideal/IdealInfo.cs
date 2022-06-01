using Newtonsoft.Json;

namespace Checkout.Apm.Ideal
{
    public class IdealInfo : HttpMetadata
    {
        [JsonProperty(PropertyName = "_links")]
        public IdealInfoLinks Links { get; set; }
    }
}
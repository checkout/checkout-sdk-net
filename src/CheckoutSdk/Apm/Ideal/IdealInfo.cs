using Newtonsoft.Json;

namespace Checkout.Apm.Ideal
{
    public class IdealInfo
    {
        [JsonProperty(PropertyName = "_links")]
        public IdealInfoLinks Links { get; set; }
    }
}
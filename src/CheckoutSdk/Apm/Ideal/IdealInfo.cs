using Newtonsoft.Json;

namespace Checkout.Apm.Ideal
{
    public sealed class IdealInfo
    {
        [JsonProperty(PropertyName = "_links")]
        public IdealInfoLinks Links { get; set; }
    }
}
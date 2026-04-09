using Newtonsoft.Json;

namespace Checkout.Accounts
{
    public class Headers : IHeaders
    {
        [JsonProperty(PropertyName = "if-match")]
        public string IfMatch { get; set; }
    }
}
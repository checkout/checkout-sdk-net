using Newtonsoft.Json;

namespace Checkout.Accounts
{
    public class Headers
    {
        [JsonProperty(PropertyName = "if-match")]
        public string IfMatch { get; set; }
    }
}
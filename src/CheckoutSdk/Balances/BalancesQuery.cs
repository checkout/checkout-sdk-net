using Newtonsoft.Json;

namespace Checkout.Balances
{
    public class BalancesQuery
    {
        public string Query { get; set; }
        
        [JsonProperty(PropertyName = "withCurrencyAccountId")]
        public bool? WithCurrencyAccountId { get; set; }
    }
}
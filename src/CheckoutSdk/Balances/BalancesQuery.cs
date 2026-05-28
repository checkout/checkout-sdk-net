using System;
using Newtonsoft.Json;

namespace Checkout.Balances
{
    public class BalancesQuery
    {
        public string Query { get; set; }

        [JsonProperty(PropertyName = "withCurrencyAccountId")]
        public bool? WithCurrencyAccountId { get; set; }

        /// <summary>
        /// A UTC datetime to retrieve historical balances at a specific point in time.
        /// Must be in the past. If omitted, the response returns live balances.
        /// [Optional]
        /// Format: date-time (RFC 3339)
        /// </summary>
        [JsonProperty(PropertyName = "balancesAt")]
        public DateTime? BalancesAt { get; set; }
    }
}

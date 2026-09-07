using System.Collections.Generic;

namespace Checkout.Balances
{
    /// <summary>
    /// The balances for each currency account (sub-account) belonging to an entity.
    /// </summary>
    public class BalancesResponse : HttpMetadata
    {
        /// <summary>
        /// The balances for each currency account that matched the query.
        /// [Optional]
        /// </summary>
        public List<CurrencyAccountBalance> Data { get; set; }
    }
}

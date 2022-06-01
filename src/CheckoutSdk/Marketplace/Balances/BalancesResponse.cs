using System.Collections.Generic;

namespace Checkout.Marketplace.Balances
{
    public class BalancesResponse : HttpMetadata
    {
        public List<CurrencyAccountBalance> Data { get; set; }
    }
}
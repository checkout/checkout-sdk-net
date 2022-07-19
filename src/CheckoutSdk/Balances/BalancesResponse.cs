using System.Collections.Generic;

namespace Checkout.Balances
{
    public class BalancesResponse : HttpMetadata
    {
        public List<CurrencyAccountBalance> Data { get; set; }
    }
}
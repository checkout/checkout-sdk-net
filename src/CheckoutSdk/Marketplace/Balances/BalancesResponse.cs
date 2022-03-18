using System.Collections.Generic;

namespace Checkout.Marketplace.Balances
{
    public class BalancesResponse
    {
        public List<CurrencyAccountBalance> Data { get; set; }
    }
}
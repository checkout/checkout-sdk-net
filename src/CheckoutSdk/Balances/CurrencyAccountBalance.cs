using Checkout.Common;

namespace Checkout.Balances
{
    public class CurrencyAccountBalance
    {
        public string Descriptor { get; set; }

        public Currency? HoldingCurrency { get; set; }

        public Balances Balances { get; set; }
    }
}
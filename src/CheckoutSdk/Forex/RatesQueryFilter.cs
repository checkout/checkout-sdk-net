namespace Checkout.Forex
{
    public class RatesQueryFilter
    {
        public string Product { get; set; }

        public ForexSource? Source { get; set; }

        public string CurrencyPairs { get; set; }

        public string ProcessChannelId { get; set; }
    }
}
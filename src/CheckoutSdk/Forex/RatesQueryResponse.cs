using System.Collections.Generic;

namespace Checkout.Forex
{
    public class RatesQueryResponse : HttpMetadata
    {
        public string Product { get; set; }

        public ForexSource? Source { get; set; }

        public IList<ForexRate> Rates { get; set; }

        public IList<string> InvalidCurrencyPairs { get; set; }
    }
}
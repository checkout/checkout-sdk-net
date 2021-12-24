using System.Collections.Generic;

namespace Checkout.Sessions
{
    public class CardInfo
    {
        public string InstrumentId { get; set; }
        public string Fingerprint { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
    }
}
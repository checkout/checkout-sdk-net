using Checkout.Common;

namespace Checkout.Forex
{
    public sealed class QuoteRequest
    {
        public Currency? SourceCurrency { get; set; }

        public long? SourceAmount { get; set; }

        public Currency? DestinationCurrency { get; set; }

        public long? DestinationAmount { get; set; }

        public string ProcessChannelId { get; set; }
    }
}
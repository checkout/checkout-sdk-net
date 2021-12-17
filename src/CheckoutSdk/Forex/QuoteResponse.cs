using Checkout.Common;
using System;

namespace Checkout.Forex
{
    public sealed class QuoteResponse
    {
        public string Id { get; set; }

        public Currency? SourceCurrency { get; set; }

        public long? SourceAmount { get; set; }

        public Currency? DestinationCurrency { get; set; }

        public long? DestinationAmount { get; set; }

        public double? Rate { get; set; }

        public DateTime? ExpiresOn { get; set; }

        public bool? IsSingleUse { get; set; }
    }
}
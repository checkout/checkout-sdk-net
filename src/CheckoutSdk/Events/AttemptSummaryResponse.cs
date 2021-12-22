using System;

namespace Checkout.Events
{
    public sealed class AttemptSummaryResponse
    {
        public int? StatusCode { get; set; }

        public string ResponseBody { get; set; }

        public string SendMode { get; set; }

        public DateTime? Timestamp { get; set; }
    }
}
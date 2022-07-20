using System;

namespace Checkout.Events.Previous
{
    public class AttemptSummaryResponse
    {
        public int? StatusCode { get; set; }

        public string ResponseBody { get; set; }

        public string SendMode { get; set; }

        public DateTime? Timestamp { get; set; }
    }
}
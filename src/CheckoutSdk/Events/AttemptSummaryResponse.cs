using System;

namespace Checkout.Events
{
    public sealed class AttemptSummaryResponse : IEquatable<AttemptSummaryResponse>
    {
        public int? StatusCode { get; set; }

        public string ResponseBody { get; set; }

        public string SendMode { get; set; }

        public DateTime? Timestamp { get; set; }

        public bool Equals(AttemptSummaryResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return StatusCode == other.StatusCode && ResponseBody == other.ResponseBody && SendMode == other.SendMode &&
                   Timestamp.Equals(other.Timestamp);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is AttemptSummaryResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StatusCode, ResponseBody, SendMode, Timestamp);
        }
    }
}
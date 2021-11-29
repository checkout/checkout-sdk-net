using System;
using System.Collections.Generic;

namespace Checkout.Events
{
    public sealed class EventsPageResponse : IEquatable<EventsPageResponse>
    {
        public int? TotalCount { get; set; }

        public int? Limit { get; set; }

        public int? Skip { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public List<EventSummaryResponse> Data { get; set; }

        public bool Equals(EventsPageResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return TotalCount == other.TotalCount && Limit == other.Limit && Skip == other.Skip &&
                   Nullable.Equals(From, other.From) && Nullable.Equals(To, other.To) && Equals(Data, other.Data);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is EventsPageResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TotalCount, Limit, Skip, From, To, Data);
        }
    }
}
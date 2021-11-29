using System;
using Checkout.Common;

namespace Checkout.Events
{
    public sealed class EventSummaryResponse : Resource, IEquatable<EventSummaryResponse>
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public DateTime? CreatedOn { get; set; }

        public bool Equals(EventSummaryResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Type == other.Type && Nullable.Equals(CreatedOn, other.CreatedOn);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is EventSummaryResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Type, CreatedOn);
        }
    }
}
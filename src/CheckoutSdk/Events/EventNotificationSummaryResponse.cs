using System;
using Checkout.Common;

namespace Checkout.Events
{
    public sealed class EventNotificationSummaryResponse : Resource, IEquatable<EventNotificationSummaryResponse>
    {
        public string Id { get; set; }

        public string Url { get; set; }

        public bool? Success { get; set; }

        public bool Equals(EventNotificationSummaryResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Url == other.Url && Success == other.Success;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is EventNotificationSummaryResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Url, Success);
        }
    }
}
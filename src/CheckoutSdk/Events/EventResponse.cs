using System;
using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Events
{
    public sealed class EventResponse : Resource, IEquatable<EventResponse>
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public string Version { get; set; }

        public DateTime? CreatedOn { get; set; }

        public IDictionary<string, object> Data { get; set; }

        public IList<EventNotificationSummaryResponse> Notifications { get; set; }

        public bool Equals(EventResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Type == other.Type && Version == other.Version &&
                   Nullable.Equals(CreatedOn, other.CreatedOn) && Equals(Data, other.Data) &&
                   Equals(Notifications, other.Notifications);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EventResponse) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Type, Version, CreatedOn, Data, Notifications);
        }
    }
}
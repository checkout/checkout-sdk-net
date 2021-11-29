using System;
using System.Collections.Generic;

namespace Checkout.Events
{
    public sealed class EventTypesResponse : IEquatable<EventTypesResponse>
    {
        public string Version { get; set; }

        public List<string> EventTypes { get; set; }

        public bool Equals(EventTypesResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Version == other.Version && Equals(EventTypes, other.EventTypes);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is EventTypesResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Version, EventTypes);
        }
    }
}
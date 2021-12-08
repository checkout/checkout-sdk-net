using System;
using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Events
{
    public sealed class EventNotificationResponse : Resource, IEquatable<EventNotificationResponse>
    {
        public string Id { get; set; }

        public string Url { get; set; }

        public bool? Success { get; set; }

        public string ContentType { get; set; }

        public IList<AttemptSummaryResponse> Attempts { get; set; }

        public bool Equals(EventNotificationResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Url == other.Url && Success == other.Success && ContentType == other.ContentType &&
                   Equals(Attempts, other.Attempts);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is EventNotificationResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Url, Success, ContentType, Attempts);
        }
    }
}
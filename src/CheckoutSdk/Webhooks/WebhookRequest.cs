using System;
using System.Collections.Generic;

namespace Checkout.Webhooks
{
    public sealed class WebhookRequest : IEquatable<WebhookRequest>
    {
        public string Url { get; set; }

        public bool Active { get; set; } = true;

        public IDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

        public WebhookContentType? ContentType { get; set; }

        public List<string> EventTypes { get; set; }

        public bool Equals(WebhookRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Url == other.Url && Active == other.Active && Equals(Headers, other.Headers) &&
                   ContentType == other.ContentType && Equals(EventTypes, other.EventTypes);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is WebhookRequest other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Url, Active, Headers, (int) ContentType, EventTypes);
        }
    }
}
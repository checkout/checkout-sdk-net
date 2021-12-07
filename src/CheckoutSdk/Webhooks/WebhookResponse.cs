using System;
using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Webhooks
{
    public sealed class WebhookResponse : Resource, IEquatable<WebhookResponse>
    {
        public string Id { get; set; }

        public string Url { get; set; }

        public bool? Active { get; set; }

        public IDictionary<string, string> Headers { get; set; }

        public WebhookContentType? ContentType { get; set; }

        public IList<string> EventTypes { get; set; }

        public bool Equals(WebhookResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Url == other.Url && Active == other.Active && Equals(Headers, other.Headers) &&
                   ContentType == other.ContentType && Equals(EventTypes, other.EventTypes);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is WebhookResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Url, Active, Headers, (int) ContentType, EventTypes);
        }
    }
}
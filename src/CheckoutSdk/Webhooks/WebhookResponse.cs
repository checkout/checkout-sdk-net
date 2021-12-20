using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Webhooks
{
    public class WebhookResponse : Resource
    {
        public string Id { get; set; }

        public string Url { get; set; }

        public bool? Active { get; set; }

        public IDictionary<string, string> Headers { get; set; }

        public WebhookContentType? ContentType { get; set; }

        public IList<string> EventTypes { get; set; }
    }
}
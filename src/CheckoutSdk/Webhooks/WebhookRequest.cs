using System.Collections.Generic;

namespace Checkout.Webhooks
{
    public class WebhookRequest
    {
        public string Url { get; set; }

        public bool Active { get; set; } = true;

        public IDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

        public WebhookContentType? ContentType { get; set; }

        public IList<string> EventTypes { get; set; }
    }
}
using System.Collections.Generic;

namespace Checkout.Webhooks
{
    public class Webhook
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }
        public IEnumerable<string> Events { get; set; }
    }
}
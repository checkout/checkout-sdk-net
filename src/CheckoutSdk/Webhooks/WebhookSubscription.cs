using System;
using System.Collections.Generic;

namespace Checkout.Webhooks
{
    /// <summary>
    /// Defines a <see cref="WebhookSubscription"/>.
    /// </summary>
    public sealed class WebhookSubscription : Webhook
    {
        /// <summary>
        /// Creates a new <see cref="WebhookSubscription"/> instance.
        /// This class is suitable for registering a new webhook.
        /// </summary>
        /// <param name="webhook">The base webhook.</param>
        public WebhookSubscription(IWebhook webhook)
        {
            if (string.IsNullOrEmpty(webhook.Url)) throw new ArgumentNullException("On registration, the URL is required.", nameof(webhook.Url));
            if (webhook.EventTypes.Count < 1) throw new ArgumentNullException("On registration, at least one event type is required.", nameof(webhook.EventTypes));

            Url = webhook.Url;
            Active = webhook.Active;
            Headers = webhook.Headers;
            EventTypes = webhook.EventTypes;
        }

        /// <summary>
        /// Creates a new <see cref="WebhookSubscription"/> instance.
        /// This class is suitable for registering a new webhook.
        /// </summary>
        /// <param name="url">The webhook receiver endpoint.</param>
        /// <param name="eventTypes">The event types for which the webhook should send notifications.</param>
        /// <param name="active">Sets the webhook to active if 'true' and deactive if 'false'. The default is 'true'.</param>
        /// <param name="headers">The headers that should be sent with every webhook notification.</param>
        public WebhookSubscription(string url, List<string> eventTypes, bool active = true, Dictionary<string, string> headers = null)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException("On registration, the URL is required.", nameof(url));
            if (eventTypes.Count < 1) throw new ArgumentNullException("On registration, at least one event type is required.", nameof(eventTypes));

            Url = url;
            Active = active;
            Headers = headers;
            EventTypes = eventTypes;
        }
    }
}

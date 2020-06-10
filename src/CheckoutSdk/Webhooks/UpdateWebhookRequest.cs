using System;
using System.Collections.Generic;

namespace Checkout.Webhooks
{
    /// <summary>
    /// Defines a <see cref="UpdateWebhookRequest"/>.
    /// </summary>
    public class UpdateWebhookRequest : Webhook
    {
        /// <summary>
        /// Creates a new <see cref="UpdateWebhookRequest"/> instance.
        /// This class is suitable for updating an existing webhook.
        /// </summary>
        /// <param name="webhook">The base webhook.</param>
        public UpdateWebhookRequest(IWebhook webhook)
        {
            if (string.IsNullOrEmpty(webhook.Url)) throw new ArgumentNullException("On update, the URL is required.", nameof(webhook.Url));
            if (!webhook.Headers.ContainsKey("Authorization")) throw new ArgumentNullException("On update, at least the 'Authorization' header is required.", nameof(webhook.Headers));
            if (webhook.EventTypes.Count < 1) throw new ArgumentNullException("On update, at least one event type is required.", nameof(webhook.EventTypes));

            Url = webhook.Url;
            Active = webhook.Active;
            Headers = webhook.Headers;
            EventTypes = webhook.EventTypes;
        }

        /// <summary>
        /// Creates a new <see cref="UpdateWebhookRequest"/> instance.
        /// This class is suitable for updating an existing webhook.
        /// </summary>
        /// <param name="url">The webhook receiver endpoint.</param>
        /// <param name="eventTypes">The event types for which the webhook should send notifications.</param>
        /// <param name="active">Sets the webhook to active if 'true' and deactive if 'false'. The default is 'true'.</param>
        /// <param name="headers">The headers that should be sent with every webhook notification.</param>
        public UpdateWebhookRequest(string url, bool active, Dictionary<string, string> headers, List<string> eventTypes)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException("On update, the URL is required.", nameof(url));
            if (!headers.ContainsKey("Authorization")) throw new ArgumentNullException("On update, at least the 'Authorization' header is required.", nameof(headers));
            if (eventTypes.Count < 1) throw new ArgumentNullException("On update, at least one event type is required.", nameof(eventTypes));

            Url = url;
            Active = active;
            Headers = headers;
            EventTypes = eventTypes;
        }
    }
}

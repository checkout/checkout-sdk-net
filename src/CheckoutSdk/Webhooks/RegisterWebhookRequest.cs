using System;
using System.Collections.Generic;

namespace Checkout.Webhooks
{
    /// <summary>
    /// Defines a <see cref="RegisterWebhookRequest"/>.
    /// </summary>
    public sealed class RegisterWebhookRequest : Webhook
    {
        /// <summary>
        /// Creates a new <see cref="RegisterWebhookRequest"/> instance.
        /// This class is suitable for registering a new webhook.
        /// </summary>
        /// <param name="webhook">The base webhook.</param>
        public RegisterWebhookRequest(IWebhook webhook)
        {
            if (string.IsNullOrEmpty(webhook.Url)) throw new ArgumentNullException(nameof(webhook.Url), "On registration, the URL is required.");
            if (webhook.EventTypes.Count < 1) throw new ArgumentNullException(nameof(webhook.EventTypes), "On registration, at least one event type is required.");

            Url = webhook.Url;
            Active = webhook.Active;
            Headers = webhook.Headers;
            EventTypes = webhook.EventTypes;
        }

        /// <summary>
        /// Creates a new <see cref="RegisterWebhookRequest"/> instance.
        /// This class is suitable for registering a new webhook.
        /// </summary>
        /// <param name="url">The webhook receiver endpoint.</param>
        /// <param name="eventTypes">The event types for which the webhook should send notifications.</param>
        /// <param name="active">Sets the webhook to active if 'true' and inactive if 'false'. The default is 'true'.</param>
        /// <param name="headers">The headers that should be sent with every webhook notification.</param>
        public RegisterWebhookRequest(string url, List<string> eventTypes, bool active = true, Dictionary<string, string> headers = null)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url), "On registration, the URL is required.");
            if (eventTypes.Count < 1) throw new ArgumentNullException(nameof(eventTypes), "On registration, at least one event type is required.");

            Url = url;
            Active = active;
            Headers = headers;
            EventTypes = eventTypes;
        }
    }
}

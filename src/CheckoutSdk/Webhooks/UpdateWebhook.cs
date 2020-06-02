using System;
using System.Collections.Generic;

namespace Checkout.Webhooks
{
    /// <summary>
    /// Defines a <see cref="UpdateWebhook"/> used in creating a <see cref="WebhookRequest{TWebhook}"/>.
    /// </summary>
    public class UpdateWebhook : Webhook, IWebhook
    {
        /// <summary>
        /// Creates a new <see cref="UpdateWebhook"/> instance.
        /// This method is suitable for registering a new webhook or updating an existing webhook.
        /// </summary>
        /// <param name="url">The webhook receiver endpoint.</param>
        /// <param name="eventTypes">The event types for which the webhook should send notifications.</param>
        /// <param name="active">Sets the webhook to active if 'true' and deactive if 'false'. The default is 'true'.</param>
        /// <param name="headers">The headers that should be sent with every webhook notification.</param>
        public UpdateWebhook(string url, bool active, Dictionary<string, string> headers, List<string> eventTypes)
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

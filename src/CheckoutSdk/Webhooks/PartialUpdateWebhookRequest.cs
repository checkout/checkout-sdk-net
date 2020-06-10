using System.Collections.Generic;

namespace Checkout.Webhooks
{
    /// <summary>
    /// Defines a <see cref="PartialUpdateWebhookRequest"/>.
    /// </summary>
    public class PartialUpdateWebhookRequest : Webhook
    {
        /// <summary>
        /// Creates a new <see cref="PartialUpdateWebhookRequest"/> instance.
        /// This class is suitable for partially updating an existing webhook.
        /// </summary>
        /// <param name="webhook">The base webhook.</param>
        public PartialUpdateWebhookRequest(IWebhook webhook)
        {
            if (!string.IsNullOrEmpty(webhook.Url)) Url = webhook.Url;
            Active = webhook.Active;
            if (webhook.Headers != null) Headers = webhook.Headers;
            if (webhook.EventTypes != null) EventTypes = webhook.EventTypes;
        }

        /// <summary>
        /// Creates a new <see cref="PartialUpdateWebhookRequest"/> instance.
        /// This class is suitable for partially updating an existing webhook.
        /// </summary>
        /// <param name="url">The webhook receiver endpoint.</param>
        /// <param name="eventTypes">The event types for which the webhook should send notifications.</param>
        /// <param name="active">Sets the webhook to active if 'true' and inactive if 'false'. The default is 'true'.</param>
        /// <param name="headers">The headers that should be sent with every webhook notification.</param>
        public PartialUpdateWebhookRequest(string url = null, bool active = true, Dictionary<string, string> headers = null, List<string> eventTypes = null)
        {
            if (!string.IsNullOrEmpty(url)) Url = url;
            Active = active;
            if (headers != null) Headers = headers;
            if (eventTypes != null) EventTypes = eventTypes;
        }
    }
}

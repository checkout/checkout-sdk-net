using System.Collections.Generic;

namespace Checkout.Webhooks
{
    /// <summary>
    /// Defines a <see cref="PartialUpdateWebhook"/> used in creating a <see cref="WebhookRequest{TWebhook}"/>.
    /// </summary>
    public class PartialUpdateWebhook : Webhook, IWebhook
    {
        /// <summary>
        /// Creates a new <see cref="PartialUpdateWebhook"/> instance.
        /// This method is suitable for registering a new webhook or updating an existing webhook.
        /// </summary>
        /// <param name="url">The webhook receiver endpoint.</param>
        /// <param name="eventTypes">The event types for which the webhook should send notifications.</param>
        /// <param name="active">Sets the webhook to active if 'true' and deactive if 'false'. The default is 'true'.</param>
        /// <param name="headers">The headers that should be sent with every webhook notification.</param>
        public PartialUpdateWebhook(string url = null, bool active = true, Dictionary<string, string> headers = null, List<string> eventTypes = null)
        {
            if (!string.IsNullOrEmpty(url)) Url = url;
            Active = active;
            if (!(headers == null)) Headers = headers;
            if (!(eventTypes == null)) EventTypes = eventTypes;
        }
    }
}

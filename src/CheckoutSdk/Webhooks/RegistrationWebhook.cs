using System;
using System.Collections.Generic;

namespace Checkout.Webhooks
{
    /// <summary>
    /// Defines a <see cref="RegistrationWebhook"/> used in creating a <see cref="WebhookRequest{TWebhook}"/>.
    /// </summary>
    public sealed class RegistrationWebhook : Webhook, IWebhook
    {
        /// <summary>
        /// Creates a new <see cref="RegistrationWebhook"/> instance.
        /// This method is suitable for registering a new webhook or updating an existing webhook.
        /// </summary>
        /// <param name="url">The webhook receiver endpoint.</param>
        /// <param name="eventTypes">The event types for which the webhook should send notifications.</param>
        /// <param name="active">Sets the webhook to active if 'true' and deactive if 'false'. The default is 'true'.</param>
        /// <param name="headers">The headers that should be sent with every webhook notification.</param>
        public RegistrationWebhook(string url, List<string> eventTypes, bool active = true, Dictionary<string, string> headers = null)
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

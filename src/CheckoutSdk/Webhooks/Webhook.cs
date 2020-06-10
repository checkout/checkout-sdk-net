using System;
using System.Collections.Generic;

namespace Checkout.Webhooks
{
    /// <summary>
    /// Defines a <see cref="Webhook"/>.
    /// </summary>
    public class Webhook : IWebhook
    {
        /// <summary>
        /// Gets or sets the webhook receiver endpoint.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets whether the webhook is active.
        /// Default is true.
        /// </summary>
        public bool Active { get; set; } = true;

        /// <summary>
        /// Gets or sets the headers to be sent with the webhook notification.
        /// This can be used to specify the "Authorization" Header present in webhook notifications.
        /// If the "Authorization" Header is not specified, the API will assign a random string automatically.
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Gets the content type that the webhook notification will be formatted in.
        /// </summary>
        [Obsolete("In a previous version this could be set to 'xml', but only 'json' is supported now.")]
        public string ContentType { get; } = "json";

        /// <summary>
        /// Gets or sets the event types for which the webhook should send notifications.
        /// </summary>
        public List<string> EventTypes { get; set; }
    }
}

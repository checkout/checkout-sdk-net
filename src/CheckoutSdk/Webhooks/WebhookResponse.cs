using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Webhooks
{
    /// <summary>
    /// Defines a <see cref="WebhookResponse"/>.
    /// </summary>
    public class WebhookResponse : Resource
    {
        /// <summary>
        /// Gets or sets the webhook identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the webhook receiver endpoint.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets whether the webhook is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the headers to be sent with the webhook notification.
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Gets the content type that the webhook notification will be formatted in.
        /// </summary>
        [Obsolete("In a previous version this could be set to 'xml', but only 'json' is supported now.")]
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the event types for which the webhook should send notifications.
        /// </summary>
        public List<string> EventTypes { get; set; }
    }
}

using System.Collections.Generic;

namespace Checkout.Events
{
    /// <summary>
    /// Defines a <see cref="EventNotificationResponse"/>.
    /// </summary>
    public class EventNotificationResponse : EventNotificationSummary
    {
        /// <summary>
        /// Gets or sets the content type of the data sent to the endpoint.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the notification events ordered by timestamp in descending order (latest first).
        /// </summary>
        public List<EventNotificationAttempt> Attempts { get; set; }
    }
}

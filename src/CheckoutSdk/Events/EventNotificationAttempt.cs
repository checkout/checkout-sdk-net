using System;

namespace Checkout.Events
{
    /// <summary>
    /// Defines a <see cref="EventNotificationAttempt"/>.
    /// </summary>
    public class EventNotificationAttempt
    {
        /// <summary>
        /// Gets or sets the the HTTP status code returned from the target server.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the response body returned from the target server.
        /// </summary>
        public string ResponseBody { get; set; }

        /// <summary>
        /// Gets or sets whether the notification was sent automatically or requested manually.
        /// </summary>
        public string SendMode { get; set; }

        /// <summary>
        /// Gets or sets the date/time the attempt was made.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}

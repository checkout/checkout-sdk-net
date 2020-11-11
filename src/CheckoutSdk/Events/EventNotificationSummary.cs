using Checkout.Common;

namespace Checkout.Events
{
    /// <summary>
    /// Defines a <see cref="EventNotificationSummary"/>.
    /// </summary>
    public class EventNotificationSummary : Resource
    {
        /// <summary>
        /// Gets or sets the unique identifier of the notification.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the url that the notification was sent to.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the success value which is true if the server response was 200 OK.
        /// </summary>
        public bool Success { get; set; }
    }
}

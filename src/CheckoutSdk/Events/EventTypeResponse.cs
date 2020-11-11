using System.Collections.Generic;

namespace Checkout.Events
{
    /// <summary>
    /// Defines an <see cref="EventTypesResponse"/>.
    /// </summary>
    public class EventTypesResponse
    {
        /// <summary>
        /// Gets or sets the version of the event types.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the event types.
        /// </summary>
        public List<string> EventTypes { get; set; }
    }
}

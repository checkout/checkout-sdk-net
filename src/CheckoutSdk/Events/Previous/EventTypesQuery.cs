namespace Checkout.Events.Previous
{
    /// <summary>
    /// Query string of the event types request.
    /// </summary>
    internal sealed class EventTypesQuery
    {
        /// <summary>
        /// The events version to retrieve the event types for.
        /// </summary>
        public string Version { get; set; }
    }
}

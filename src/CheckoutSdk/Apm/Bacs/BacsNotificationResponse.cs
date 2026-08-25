namespace Checkout.Apm.Bacs
{
    /// <summary>
    /// Bacs Direct Debit pre-notification response.
    /// </summary>
    public class BacsNotificationResponse : HttpMetadata
    {
        /// <summary>
        /// The unique identifier of the notification event.
        /// [Required]
        /// </summary>
        public string EventId { get; set; }
    }
}

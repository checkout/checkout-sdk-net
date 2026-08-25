using Checkout.Common;

namespace Checkout.Apm.Bacs
{
    /// <summary>
    /// Bacs Direct Debit notification request.
    /// </summary>
    public class BacsNotificationRequest
    {
        /// <summary>
        /// The ID of the Bacs Direct Debit instrument to notify against.
        /// [Required]
        /// Pattern: ^(src)_(\w{26})$
        /// </summary>
        public string SourceId { get; set; }

        /// <summary>
        /// The type of pre-notification being sent to the payer.
        /// [Required]
        /// </summary>
        public BacsNotificationType? NotificationType { get; set; }

        /// <summary>
        /// The date the funds will be collected from the payer's account, in the format yyyy-MM-dd.
        /// [Required]
        /// Format: yyyy-MM-dd
        /// </summary>
        public string CollectionDate { get; set; }

        /// <summary>
        /// The amount to be collected, in the currency's minor unit.
        /// [Required]
        /// Format: int64
        /// min 1
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// The three-letter ISO 4217 currency code of the collection.
        /// [Required]
        /// min 3 characters, max 3 characters
        /// </summary>
        public Currency? Currency { get; set; }

        /// <summary>
        /// A reference you can use to identify the collection.
        /// [Optional]
        /// max 50 characters
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// The email address of the payer that the pre-notification is sent to.
        /// [Required]
        /// Format: email
        /// </summary>
        public string CustomerEmail { get; set; }

        /// <summary>
        /// The billing descriptor that appears on the payer's bank statement.
        /// [Required]
        /// max 25 characters
        /// </summary>
        public string BillingDescriptor { get; set; }

        /// <summary>
        /// The support email address included in the pre-notification.
        /// [Required]
        /// Format: email
        /// </summary>
        public string SupportEmail { get; set; }

        /// <summary>
        /// The support phone number included in the pre-notification, in E.164 format.
        /// [Optional]
        /// </summary>
        public string SupportPhone { get; set; }
    }
}

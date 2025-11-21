using System;

namespace Checkout.AgenticCommerce.Common
{
    /// <summary>
    /// Mandate configuration for purchase intents
    /// </summary>
    public class MandateExtended: MandateBase
    {
        /// <summary>
        /// The unique identifier for the mandate
        /// [Required]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Purchase threshold configuration
        /// </summary>
        public PurchaseThreshold PurchaseThreshold { get; set; }

        /// <summary>
        /// A brief description of the purchase intent
        /// &lt;= 255 characters
        /// [Required]
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The date and time when the purchase intent expires, in ISO 8601 format.
        /// This value must be set to a date and time in the future
        /// [Required]
        /// </summary>
        public DateTime? ExpirationDate { get; set; }
    }
}
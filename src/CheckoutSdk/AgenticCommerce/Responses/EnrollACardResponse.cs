using System;

namespace Checkout.AgenticCommerce.Responses
{
    /// <summary>
    /// Card enrolled successfully
    /// </summary>
    public class EnrollACardResponse : HttpMetadata
    {
        /// <summary>
        /// The unique identifier for the provisioned token
        /// [Required]
        /// </summary>
        public string TokenId { get; set; }

        /// <summary>
        /// The status of the enrollment
        /// Value: "enrolled"
        /// [Required]
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The date and time the enrollment was created, in ISO 8601 format
        /// [Optional]
        /// </summary>
        public DateTime? CreatedAt { get; set; }
    }
}
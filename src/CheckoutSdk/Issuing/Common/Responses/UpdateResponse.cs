using Checkout.Common;
using System;

namespace Checkout.Issuing.Common.Responses
{
    /// <summary>
    /// The response returned when a cardholder's details are updated.
    /// </summary>
    public class UpdateResponse : Resource
    {
        /// <summary>
        /// The date and time when the cardholder was last modified, in UTC.
        /// [Required]
        /// Format: date-time (RFC 3339)
        /// </summary>
        public DateTime? LastModifiedDate { get; set; }
    }
}

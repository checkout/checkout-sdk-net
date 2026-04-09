using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.GooglePay.Entities;
using System;

namespace Checkout.HandlePaymentsAndPayouts.GooglePay.Responses
{
    /// <summary>
    /// Response returned after enrolling an entity with Google Pay.
    /// Required (API): tosAcceptedTime, state.
    /// </summary>
    public class GooglePayEnrollmentResponse : Resource
    {
        /// <summary>
        /// An ISO 8601 timestamp of when the Google terms of service were accepted.
        /// [Required]
        /// Format: date-time
        /// </summary>
        public DateTime? TosAcceptedTime { get; set; }

        /// <summary>
        /// The current enrollment state of the entity.
        /// [Required]
        /// Enum: ACTIVE, INACTIVE
        /// </summary>
        public GooglePayEnrollmentState? State { get; set; }
    }
}

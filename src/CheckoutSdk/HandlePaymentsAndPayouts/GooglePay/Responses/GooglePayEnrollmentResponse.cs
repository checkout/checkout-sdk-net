using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.GooglePay.Entities;
using System;

namespace Checkout.HandlePaymentsAndPayouts.GooglePay.Responses
{
    /// <summary>
    /// Response returned after enrolling an entity with Google Pay.
    /// <para>
    /// The real 201 body is { merchant_id, tos_accepted_time, state }. The spec declares only
    /// tosAcceptedTime and state, with additionalProperties false, so merchant_id was missing
    /// here: the class was generated faithfully from a wrong schema. Reported by a merchant.
    /// The spec is being fixed separately; until then this class follows the live API.
    /// </para>
    /// </summary>
    public class GooglePayEnrollmentResponse : Resource
    {
        /// <summary>
        /// The Google Pay merchant identifier assigned to the entity, needed to initialise
        /// Google Pay on the client. Returned by the API but absent from the spec.
        /// </summary>
        public string MerchantId { get; set; }

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

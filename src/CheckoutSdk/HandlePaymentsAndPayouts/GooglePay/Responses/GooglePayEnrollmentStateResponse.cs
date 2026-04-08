using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.GooglePay.Entities;

namespace Checkout.HandlePaymentsAndPayouts.GooglePay.Responses
{
    /// <summary>
    /// Response containing the current enrollment state of the entity with Google Pay.
    /// Required (API): state.
    /// </summary>
    public class GooglePayEnrollmentStateResponse : Resource
    {
        /// <summary>
        /// The current enrollment state of the entity.
        /// [Required]
        /// Enum: ACTIVE, INACTIVE
        /// </summary>
        public GooglePayEnrollmentState State { get; set; }
    }
}

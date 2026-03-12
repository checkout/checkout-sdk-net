using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.Flow.Entities;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Responses
{
    public abstract class PaymentSubmissionResponse : Resource
    {
        /// <summary>
        /// The payment identifier.
        /// [Required]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The payment's status.
        /// [Required]
        /// </summary>
        public abstract string Status { get; set; }

        /// <summary>
        /// The payment method name.
        /// [Required]
        /// </summary>
        public PaymentMethod? Type { get; set; }
    }
}
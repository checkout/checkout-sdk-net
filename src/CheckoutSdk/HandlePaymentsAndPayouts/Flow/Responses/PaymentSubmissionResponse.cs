using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.Flow.Entities;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Responses
{
    public class PaymentSubmissionResponse : Resource
    {
        /// <summary>
        /// The payment identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The payment's status. One of: "Approved", "Declined", "Action Required".
        /// </summary>
        public virtual string Status { get; set; }

        /// <summary>
        /// The payment method name.
        /// </summary>
        public PaymentMethod? Type { get; set; }

        /// <summary>
        /// The reason for a declined payment. Present when Status is "Declined".
        /// </summary>
        public string DeclineReason { get; set; }

        /// <summary>
        /// Instruction for further payment action. Present when Status is "Action Required".
        /// </summary>
        public object Action { get; set; }

        /// <summary>
        /// The Payment Sessions unique identifier. Present in CreateAndSubmitPaymentSession responses.
        /// </summary>
        public string PaymentSessionId { get; set; }

        /// <summary>
        /// The secret used by Flow to authenticate payment session requests. Present in CreateAndSubmitPaymentSession responses.
        /// Do not log or store this value.
        /// </summary>
        public string PaymentSessionSecret { get; set; }
    }
}
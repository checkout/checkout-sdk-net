using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.Flow.Entities;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Responses
{
    public class ActionRequiredPaymentSubmissionResponse : PaymentSubmissionResponse
    {
        /// <summary>
        /// The payment's status.
        /// </summary>
        public override string Status { get; set; } = "Action Required";

        /// <summary>
        /// Instruction for further payment action.
        /// </summary>
        public object Action { get; set; }

        /// <summary>
        /// The Payment Sessions unique identifier (only present in CreateAndSubmitPaymentSession response)
        /// </summary>
        public string PaymentSessionId { get; set; }

        /// <summary>
        /// The secret used by Flow to authenticate payment session requests (only present in CreateAndSubmitPaymentSession response).
        /// Do not log or store this value.
        /// </summary>
        public string PaymentSessionSecret { get; set; }
    }
}
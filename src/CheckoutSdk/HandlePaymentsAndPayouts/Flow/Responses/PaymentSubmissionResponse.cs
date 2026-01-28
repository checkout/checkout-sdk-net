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

    public class ApprovedPaymentSubmissionResponse : PaymentSubmissionResponse
    {
        /// <summary>
        /// The payment's status.
        /// </summary>
        public override string Status { get; set; } = "Approved";

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

    public class DeclinedPaymentSubmissionResponse : PaymentSubmissionResponse
    {
        /// <summary>
        /// The payment's status.
        /// </summary>
        public override string Status { get; set; } = "Declined";

        /// <summary>
        /// The reason for the payment decline.
        /// </summary>
        public string DeclineReason { get; set; }
    }

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
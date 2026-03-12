using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.Flow.Entities;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Responses
{
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
}
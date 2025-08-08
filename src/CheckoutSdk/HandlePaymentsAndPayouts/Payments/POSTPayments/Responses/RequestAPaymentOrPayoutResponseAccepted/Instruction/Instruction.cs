using System;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseAccepted.
    Instruction
{
    public class Instruction
    {
        /// <summary>
        /// The date (in ISO 8601 format) and time at which the recipient's account will be credited.
        /// <date-time>
        /// </summary>
        public DateTime? ValueDate { get; set; }
    }
}
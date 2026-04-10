using System.Collections.Generic;

namespace Checkout.Payments.Request
{
    public class PaymentRouting
    {
        /// <summary>
        /// Specifies the processing rules for the payment. Each object in the array
        /// should be a unique expression of rules that determine the routing attempts
        /// to process the payment with.
        /// [Optional]
        /// </summary>
        public IList<PaymentRoutingAttempt> Attempts { get; set; }
    }
}

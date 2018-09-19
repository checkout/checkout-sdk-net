namespace Checkout.Payments
{
    public class PaymentResponse
    {
        /// <summary>
        /// HTTP Status Code (201): Payment processed successfully
        /// </summary>
        public PaymentProcessed Payment { get; set; }
        /// <summary>
        /// HTTP Status Code (202): Payment asynchronous or further action required
        /// </summary>
        public PaymentPending Pending { get; set; }

        public bool IsPending => Pending != null;

        public static implicit operator PaymentResponse(PaymentPending pending)
        {
            return new PaymentResponse { Pending = pending };
        }

        public static implicit operator PaymentResponse(PaymentProcessed payment)
        {
            return new PaymentResponse { Payment = payment };
        }
    }
}
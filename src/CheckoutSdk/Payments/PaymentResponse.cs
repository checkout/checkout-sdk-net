namespace Checkout.Sdk.Payments
{
    public class PaymentResponse
    {
        public PaymentPending Pending { get; set; }
        public PaymentProcessed Payment { get; set; }

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
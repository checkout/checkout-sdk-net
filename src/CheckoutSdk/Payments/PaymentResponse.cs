namespace Checkout.Payments
{
    public class PaymentResponse<TSource>
    {
        public PaymentPending Pending { get; set; }
        public PaymentProcessed<TSource> Payment { get; set; }

        public bool IsAccepted => Pending != null;

        public static implicit operator PaymentResponse<TSource>(PaymentPending pending)
        {
            return new PaymentResponse<TSource> { Pending = pending };
        }

        public static implicit operator PaymentResponse<TSource>(PaymentProcessed<TSource> payment)
        {
            return new PaymentResponse<TSource> { Payment = payment };
        }
    }
}
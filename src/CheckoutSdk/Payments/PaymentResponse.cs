namespace Checkout.Payments
{
    public class PaymentResponse<TSource>
    {
        public PaymentAccepted Accepted { get; set; }
        public PaymentProcessed<TSource> Processed { get; set; }

        public bool IsAccepted => Accepted != null;
        public bool IsProcessed => Processed != null;

        public static explicit operator PaymentResponse<TSource>(PaymentProcessed<TSource> paymentProcessed)
        {
            return new PaymentResponse<TSource> { Processed = paymentProcessed };
        }

        public static explicit operator PaymentResponse<TSource>(PaymentAccepted paymentAccepted)
        {
            return new PaymentResponse<TSource> { Accepted = paymentAccepted };
        }
    }
}
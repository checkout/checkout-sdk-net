namespace Checkout.Payments
{
    public class PaymentRequest<TSource> where TSource : IPaymentSource
    {
        public PaymentRequest(TSource source, int? amount, string currency)
        {
            Source = source;
            Amount = amount;
            Currency = currency;
        }

        public int? Amount { get; }
        public string Currency { get; }
        public TSource Source { get; }
    }
}
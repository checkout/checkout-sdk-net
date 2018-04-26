namespace Checkout.Payments
{
    public class AlternativePaymentRequest : PaymentRequest<AlternativePaymentSource>
    {
        public AlternativePaymentRequest(int? amount, string currency, AlternativePaymentSource source) : base(amount, currency, source)
        {
        }
    }
}
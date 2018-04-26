namespace Checkout.Payments
{
    public class CardPaymentRequest : PaymentRequest<CardSource>
    {
        public CardPaymentRequest(int? amount, string currency, CardSource source) : base(amount, currency, source)
        {
            
        }
    }
}
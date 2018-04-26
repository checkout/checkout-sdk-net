namespace Checkout.Payments
{
    public class TokenPaymentRequest : PaymentRequest<TokenSource>
    {
        public TokenPaymentRequest(int? amount, string currency, TokenSource source) : base(amount, currency, source)
        {
        }
    }
}
namespace Checkout.Payments
{
    public class CardPaymentRequest : PaymentRequest<CardSource>
    {
        public CardPaymentRequest(CardSource card, int amount, string currency)
         : base(card, amount, currency)
        {

        }
    }
}
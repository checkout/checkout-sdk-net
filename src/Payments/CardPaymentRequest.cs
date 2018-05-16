namespace Checkout.Payments
{
    public class CardPaymentRequest
    {
        public CardPaymentRequest(int amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }
        
        public int Amount {get;set;}
        public string Currency {get;set;}
    }
}
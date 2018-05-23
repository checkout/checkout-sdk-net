namespace Checkout.Payments
{
    public class CardSource : IPaymentSource
    {
        public CardSource(string number, int expiryMonth, int expiryYear) 
        {
            Number = number;
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
        }
        
        public string Number {get;}
        public int ExpiryMonth {get;}
        public int ExpiryYear {get;}

        public string Type => "card";
    }
}
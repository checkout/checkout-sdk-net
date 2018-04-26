using System;

namespace Checkout.Payments
{
    public class CardSource : PaymentSource
    {
        public CardSource(string number, int expiryMonth, int expiryYear)
            : base("card")
        {
            Number = number ?? throw new ArgumentNullException(nameof(number));
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
        }
        
        public string Number {get;}
        public int ExpiryMonth {get;}
        public int ExpiryYear {get;}
    }
}
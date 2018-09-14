using Checkout.Common;

namespace Checkout.Payments
{
    public class CardSource : IPaymentSource
    {
        public const string TypeName = "card";

        public CardSource(string number, int expiryMonth, int expiryYear)
        {
            Number = number;
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
        }

        public string Number { get; }
        public int ExpiryMonth { get; }
        public int ExpiryYear { get; }
        public string Name { get; set; }
        public string Cvv { get; set; }
        public Address BillingAddress { get; set; }
        public Phone Phone { get; set; }

        public string Type => TypeName;
    }
}
using Checkout.Sdk.Common;

namespace Checkout.Sdk.Tokens
{
    public class CardTokenRequest : ITokenRequest
    {
        public CardTokenRequest(string number, int expiryMonth, int expiryYear)
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

        public string Type => "card";
    }
}
using Checkout.Common;

namespace Checkout.Tokens
{
    public sealed class CardTokenRequest 
    {
        public readonly TokenType Type = TokenType.Card;

        public string Number { get; set; }

        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public string Name { get; set; }

        public string Cvv { get; set; }

        public Address BillingAddress { get; set; }

        public Phone Phone { get; set; }
             
    }
}
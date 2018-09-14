using Checkout.Common;

namespace Checkout.Tokens
{
    public class CardTokenResponse : TokenResponse
    {
        public Address BillingAddress {get;set;}
        public Phone Phone { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string Name { get; set; }
        public string Scheme { get; set; }
        public string Last4 { get; set; }
        public string Bin { get; set; }
        public string CardType { get; set; }
        public string CardCategory { get; set; }
        public string Issuer { get; set; }
        public string IssuerCountry { get; set; }
        public string ProductId { get; set; }
        public string ProductType { get; set; }
    }
}
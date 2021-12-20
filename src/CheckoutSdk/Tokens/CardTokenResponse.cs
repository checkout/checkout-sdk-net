using Checkout.Common;

namespace Checkout.Tokens
{
    public class CardTokenResponse : TokenResponse
    {
        public Address BillingAddress { get; set; }

        public Phone Phone { get; set; }

        public string Name { get; set; }
    }
}
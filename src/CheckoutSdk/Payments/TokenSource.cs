using System;

namespace Checkout.Payments
{
    public class TokenSource : PaymentSource
    {
        public TokenSource(string token) : base("token")
        {
            Token = token ?? throw new ArgumentNullException(nameof(token));
        }

        public string Token {get;}
    }
}
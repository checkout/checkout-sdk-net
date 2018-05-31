using System;

namespace Checkout.Tokens
{
    public class TokenResponse
    {
        public string Type { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
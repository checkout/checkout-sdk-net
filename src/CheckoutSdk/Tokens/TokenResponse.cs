using System;
using Checkout.Sdk.Common;

namespace Checkout.Sdk.Tokens
{
    public class TokenResponse : Resource
    {
        public string Type { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
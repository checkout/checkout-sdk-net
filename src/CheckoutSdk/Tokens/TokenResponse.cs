using System;
using Checkout.Common;

namespace Checkout.Tokens
{
    public class TokenResponse : Resource
    {
        /// <summary>
        /// The type of card details to be tokenized
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// The reference token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// The date/time the token will expire
        /// </summary>
        public DateTime ExpiresOn { get; set; }
    }
}
using System;
using Checkout.Common;

namespace Checkout.Tokens
{
    /// <summary>
    /// Indicates successful token creation containing the token details.
    /// </summary>
    public class TokenResponse : Resource
    {
        /// <summary>
        /// Gets the token type.
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// Gets the reference token.
        /// </summary>
        public string Token { get; set; }
        
        /// <summary>
        /// Gets the date/time the token will expire.
        /// </summary>
        public DateTime ExpiresOn { get; set; }
    }
}
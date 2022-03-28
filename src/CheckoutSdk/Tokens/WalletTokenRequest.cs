using System.Collections.Generic;

namespace Checkout.Tokens
{
    /// <summary>
    /// Defines a request to exchange a digital wallet token for a reference token that can later be used to initiate a payment via a <see cref="TokenSource"/>.
    /// </summary>
    public class WalletTokenRequest : ITokenRequest
    {
        /// <summary>
        /// Creates a new <see cref="WalletTokenRequest"/> instance.
        /// </summary>
        /// <param name="tokenType">The token type.</param>
        /// <param name="tokenData">The wallet token data.</param>
        public WalletTokenRequest(TokenType tokenType, IDictionary<string, object> tokenData)
        {
            Type = tokenType;
            TokenData = tokenData;
        }

        /// <summary>
        /// Gets the type of token.
        /// </summary>
        public TokenType Type { get; }

        /// <summary>
        /// Gets the wallet (Google Pay, Apple Pay etc) payment token data.
        /// </summary>
        public IDictionary<string, object> TokenData { get; }
    }
}
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
        /// <param name="walletType">The wallet type.</param>
        /// <param name="tokenData">The wallet token data.</param>
        public WalletTokenRequest(WalletType walletType, Dictionary<string, object> tokenData)
        {
            Type = walletType.ToString();
            TokenData = tokenData;
        }

        /// <summary>
        /// Gets the type of token.
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Gets the wallet (Google Pay, Apple Pay etc) payment token data.
        /// </summary>
        public Dictionary<string, object> TokenData { get; }
    }
}

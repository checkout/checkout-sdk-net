using System.Collections.Generic;

namespace Checkout.Tokens
{
    public class WalletTokenRequest : ITokenRequest
    {
        public WalletTokenRequest(string type, Dictionary<string, object> tokenData)
        {
            Type = type;
            TokenData = tokenData;
        }

        public string Type { get; }
        /// <summary>
        /// The Wallet (Google Pay, Apple Pay etc) Payment Token
        /// </summary>
        public Dictionary<string, object> TokenData { get; }
    }
}
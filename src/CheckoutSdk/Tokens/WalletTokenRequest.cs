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
        public Dictionary<string, object> TokenData { get; }
    }
}
using Newtonsoft.Json;

namespace Checkout.Tokens
{
    public class GooglePayTokenRequest : WalletTokenRequest
    {
        public GooglePayTokenRequest() : base(TokenType.GooglePay)
        {
        }

        [JsonProperty("token_data")] public GooglePayTokenData TokenData { get; set; }
    }
}
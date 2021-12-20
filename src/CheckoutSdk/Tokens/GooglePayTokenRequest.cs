using Newtonsoft.Json;

namespace Checkout.Tokens
{
    public class GooglePayTokenRequest : WalletTokenRequest
    {
        public GooglePayTokenRequest() : base(TokenType.GooglePay)
        {
        }

        [JsonProperty("token_data")] public ApplePayTokenData TokenData { get; set; }
    }
}
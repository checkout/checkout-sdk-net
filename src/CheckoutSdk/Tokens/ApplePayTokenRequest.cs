using Newtonsoft.Json;

namespace Checkout.Tokens
{
    public sealed class ApplePayTokenRequest : WalletTokenRequest
    {
        public ApplePayTokenRequest() : base(TokenType.ApplePay)
        {
        }

        [JsonProperty("token_data")] public ApplePayTokenData TokenData { get; set; }
    }
}
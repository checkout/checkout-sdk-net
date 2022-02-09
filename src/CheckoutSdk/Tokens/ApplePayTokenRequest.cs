#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif

namespace Checkout.Tokens
{
    public class ApplePayTokenRequest : WalletTokenRequest
    {
        public ApplePayTokenRequest() : base(TokenType.ApplePay)
        {
        }

#if NET5_0_OR_GREATER
        [JsonPropertyName("token_data")]
#else
        [JsonProperty(PropertyName = "token_data")]
#endif
        public ApplePayTokenData TokenData { get; set; }
    }
}
#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif

namespace Checkout.Tokens
{
    public class GooglePayTokenData
    {
        public string Signature { get; set; }

#if NET5_0_OR_GREATER
        [JsonPropertyName("protocolVersion")]
#else
        [JsonProperty(PropertyName = "protocolVersion")]
#endif
        public string ProtocolVersion { get; set; }

#if NET5_0_OR_GREATER
        [JsonPropertyName("signedMessage")]
#else
        [JsonProperty(PropertyName = "signedMessage")]
#endif
        public string SignedMessage { get; set; }
    }
}
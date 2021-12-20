using Newtonsoft.Json;

namespace Checkout.Tokens
{
    public class GooglePayTokenData
    {
        public string Signature { get; set; }

        [JsonProperty(PropertyName = "protocolVersion")]
        public string ProtocolVersion { get; set; }

        [JsonProperty(PropertyName = "signedMessage")]
        public string SignedMessage { get; set; }
    }
}
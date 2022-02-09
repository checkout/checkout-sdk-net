#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif
using System.Collections.Generic;

namespace Checkout.Tokens
{
    public class ApplePayTokenData
    {
        public string Version { get; set; }

        public string Data { get; set; }

        public string Signature { get; set; }

#if NET5_0_OR_GREATER
        [JsonPropertyName("header")]
#else
        [JsonProperty(PropertyName = "header")]
#endif
        private IDictionary<string, string> TokenHeader { get; set; }
    }
}
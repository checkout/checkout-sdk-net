using Newtonsoft.Json;
using System.Collections.Generic;

namespace Checkout.Tokens
{
    public class ApplePayTokenData
    {
        public string Version { get; set; }

        public string Data { get; set; }

        public string Signature { get; set; }

        [JsonProperty("header")] public IDictionary<string, string> TokenHeader { get; set; }
    }
}
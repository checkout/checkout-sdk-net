using Newtonsoft.Json;
using System.Collections.Generic;

namespace Checkout.Tokens
{
    public sealed class ApplePayTokenData 
    {
        public string Version { get; set; }

        public string Data { get; set; }

        public string Signature { get; set; }

        [JsonProperty("header")] private IDictionary<string, string> TokenHeader { get; set; }
       
    }
}
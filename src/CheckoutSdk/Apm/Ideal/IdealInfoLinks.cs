using Checkout.Common;
#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif
using System.Collections.Generic;

namespace Checkout.Apm.Ideal
{
    public class IdealInfoLinks
    {
        public Link Self { get; set; }

        public IList<CuriesLink> Curies { get; set; }

#if NET5_0_OR_GREATER
        [JsonPropertyName("ideal:issuers")]
#else
        [JsonProperty(PropertyName = "ideal:issuers")]
#endif
        public Link Issuers { get; set; }
    }
}
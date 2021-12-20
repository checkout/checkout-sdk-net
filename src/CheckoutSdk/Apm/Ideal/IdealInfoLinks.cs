using Checkout.Common;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Checkout.Apm.Ideal
{
    public class IdealInfoLinks
    {
        public Link Self { get; set; }

        public IList<CuriesLink> Curies { get; set; }

        [JsonProperty(PropertyName = "ideal:issuers")]
        public Link Issuers { get; set; }
    }
}
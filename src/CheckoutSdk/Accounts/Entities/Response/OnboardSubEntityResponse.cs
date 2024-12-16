using Newtonsoft.Json;
using System.Collections.Generic;

namespace Checkout.Accounts.Entities.Response
{
    public class OnboardSubEntityResponse : HttpMetadata
    {
        [JsonExtensionData]
        public IDictionary<string, object> Response { get; set; }
    }
}
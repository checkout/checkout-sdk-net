using Newtonsoft.Json;
using System.Collections.Generic;

namespace Checkout.Accounts.Entities.Request
{
    public class OnboardSubEntityRequest
    {
        [JsonExtensionData]
        public IDictionary<string, object> Request { get; set; }
    }
}
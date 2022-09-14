using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Reports
{
    public class FileResponse : Resource
    {
        public string Id { get; set; }
        
        [JsonProperty(PropertyName = "filename")]
        public string FileName { get; set; }
        
        public string Format { get; set; }
    }
}

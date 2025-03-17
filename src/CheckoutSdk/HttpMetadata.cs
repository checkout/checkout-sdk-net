using System.Collections.Generic;

namespace Checkout
{
    public class HttpMetadata
    {
        public int? HttpStatusCode { get; set; }

        public string Body { get; set; }

        public IDictionary<string, string> ResponseHeaders { get; set; }
    }
    
    public class DefaultHttpMetadata : HttpMetadata
    {
        public DefaultHttpMetadata()
        {
            Body = string.Empty;
            HttpStatusCode = 0;
            ResponseHeaders = new Dictionary<string, string>();
        }
    }
}
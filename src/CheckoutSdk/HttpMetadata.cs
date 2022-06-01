using System.Collections.Generic;

namespace Checkout
{
    public class HttpMetadata
    {
        public int? HttpStatusCode { get; set; }

        public string Body { get; set; }

        public IDictionary<string, string> ResponseHeaders { get; set; }
    }
}
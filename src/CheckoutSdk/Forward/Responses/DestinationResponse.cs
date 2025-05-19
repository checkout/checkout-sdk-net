using System.Collections.Generic;

namespace Checkout.Forward.Responses
{
    public class DestinationResponse
    {
        /// <summary> The HTTP status code of the destination response (Required) </summary>
        public int Status { get; set; }

        /// <summary> The destination response's HTTP headers. (Required) </summary>
        public IDictionary<string, IList<string>> Headers { get; set; } = new Dictionary<string, IList<string>>();

        /// <summary> The destination response's HTTP message body (Required) </summary>
        public string Body { get; set; }
    }
}
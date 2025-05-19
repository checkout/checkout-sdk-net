using System.Collections.Generic;

namespace Checkout.Forward.Responses
{
    public class DestinationRequest
    {
        /// <summary> The URL of the forward request (Required) </summary>
        public string Url { get; set; }

        /// <summary> The HTTP method of the forward request (Required) </summary>
        public string Method { get; set; }

        /// <summary> The HTTP headers of the forward request. Encrypted and sensitive header values are redacted (Required) </summary>
        public IDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     The HTTP message body of the forward request. This is the original value used to initiate the request, with
        ///     placeholder value text included. For example, {{card_number}} is not replaced with an actual card number (Required)
        /// </summary>
        public string Body { get; set; }
    }
}
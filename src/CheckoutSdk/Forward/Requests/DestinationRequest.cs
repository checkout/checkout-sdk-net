namespace Checkout.Forward.Requests
{
    public class DestinationRequest
    {
        /// <summary> The URL to forward the request to (Required, max 1024 characters) </summary>
        public string Url { get; set; }

        /// <summary> The HTTP method to use for the forward request (Required) </summary>
        public MethodType Method { get; set; }

        /// <summary> The HTTP headers to include in the forward request (Required) </summary>
        public Headers Headers { get; set; }

        /// <summary>
        ///     The HTTP message body to include in the forward request. If you provide source.id or source.token, you can specify
        ///     placeholder values in the body. The request will be enriched with the respective payment details from the token or
        ///     payment instrument you specified. For example, {{card_number}} (Required, max 16384 characters)
        /// </summary>
        public string Body { get; set; }
    }
}
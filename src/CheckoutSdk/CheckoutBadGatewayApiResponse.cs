using System.Net;
using System.Net.Http.Headers;

namespace Checkout
{
    /// <summary>
    /// Checkout API HTTP response message following a HTTP 502 (Bad Gateway) response.
    /// </summary>
    public class CheckoutBadGatewayApiResponse : CheckoutApiHttpResponseMessage
    {
        /// <summary>
        /// Creates a new <see cref="CheckoutBadGatewayApiResponse"/> instance.
        /// </summary>
        /// <param name="httpResponseHeaders">The headers of the API response.</param>
        public CheckoutBadGatewayApiResponse(HttpResponseHeaders httpResponseHeaders = null) 
            : base(HttpStatusCode.BadGateway, httpResponseHeaders) { }
    }
}

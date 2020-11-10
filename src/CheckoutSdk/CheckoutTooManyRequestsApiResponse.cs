using System.Net;
using System.Net.Http.Headers;

namespace Checkout
{
    /// <summary>
    /// Checkout API HTTP response message following a HTTP 429 (Too Many Requests) response.
    /// </summary>
    public class CheckoutTooManyRequestsApiResponse : CheckoutApiHttpResponseMessage
    {
        /// <summary>
        /// Creates a new <see cref="CheckoutTooManyRequestsApiResponse"/> instance.
        /// </summary>
        /// <param name="httpResponseHeaders">The headers of the API response.</param>
        public CheckoutTooManyRequestsApiResponse(HttpResponseHeaders httpResponseHeaders = null) 
            : base((HttpStatusCode)429, httpResponseHeaders) { }
    }
}

using System.Net;
using System.Net.Http.Headers;

namespace Checkout
{
    /// <summary>
    /// Checkout API HTTP response message following a HTTP 200 (OK) response.
    /// </summary>
    public class CheckoutOkApiResponse : CheckoutApiHttpResponseMessage
    {
        /// <summary>
        /// Creates a new <see cref="CheckoutOkApiResponse"/> instance.
        /// </summary>
        /// <param name="httpResponseHeaders">The headers of the API response.</param>
        public CheckoutOkApiResponse(HttpResponseHeaders httpResponseHeaders = null) 
            : base(HttpStatusCode.OK, httpResponseHeaders) { }
    }
}

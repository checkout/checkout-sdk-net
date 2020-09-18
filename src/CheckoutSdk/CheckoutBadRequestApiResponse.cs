using System.Net;
using System.Net.Http.Headers;

namespace Checkout
{
    /// <summary>
    /// Checkout API HTTP response message following a HTTP 400 (Bad Request) response.
    /// </summary>
    public class CheckoutBadRequestApiResponse : CheckoutApiHttpResponseMessage
    {
        /// <summary>
        /// Creates a new <see cref="CheckoutBadRequestApiResponse"/> instance.
        /// </summary>
        /// <param name="httpResponseHeaders">The headers of the API response.</param>
        public CheckoutBadRequestApiResponse(HttpResponseHeaders httpResponseHeaders = null) 
            : base(HttpStatusCode.BadRequest, httpResponseHeaders) { }
    }
}

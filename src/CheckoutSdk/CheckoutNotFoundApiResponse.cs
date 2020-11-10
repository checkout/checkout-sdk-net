using System.Net;
using System.Net.Http.Headers;

namespace Checkout
{
    /// <summary>
    /// Checkout API HTTP response message following a HTTP 404 (Not Found) response.
    /// </summary>
    public class CheckoutNotFoundApiResponse : CheckoutApiHttpResponseMessage
    {
        /// <summary>
        /// Creates a new <see cref="CheckoutNotFoundApiResponse"/> instance.
        /// </summary>
        /// <param name="httpResponseHeaders">The headers of the API response.</param>
        public CheckoutNotFoundApiResponse(HttpResponseHeaders httpResponseHeaders = null) 
            : base(HttpStatusCode.NotFound, httpResponseHeaders) { }
    }
}

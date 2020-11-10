using System.Net;
using System.Net.Http.Headers;

namespace Checkout
{
    /// <summary>
    /// Checkout API HTTP response message following a HTTP 204 (No Content) response.
    /// </summary>
    public class CheckoutNoContentApiResponse : CheckoutApiHttpResponseMessage
    {
        /// <summary>
        /// Creates a new <see cref="CheckoutNoContentApiResponse"/> instance.
        /// </summary>
        /// <param name="httpResponseHeaders">The headers of the API response.</param>
        public CheckoutNoContentApiResponse(HttpResponseHeaders httpResponseHeaders = null) 
            : base(HttpStatusCode.NoContent, httpResponseHeaders) { }
    }
}

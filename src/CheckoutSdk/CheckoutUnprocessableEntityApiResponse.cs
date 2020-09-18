using System.Net;
using System.Net.Http.Headers;

namespace Checkout
{
    /// <summary>
    /// Checkout API HTTP response message following a HTTP 422 (Unprocessable Entity) response.
    /// </summary>
    public class CheckoutUnprocessableEntityApiResponse : CheckoutApiHttpResponseMessage
    {
        /// <summary>
        /// Creates a new <see cref="CheckoutUnprocessableEntityApiResponse"/> instance.
        /// </summary>
        /// <param name="httpResponseHeaders">The headers of the API response.</param>
        public CheckoutUnprocessableEntityApiResponse(HttpResponseHeaders httpResponseHeaders = null) 
            : base((HttpStatusCode)422, httpResponseHeaders) { }
    }
}

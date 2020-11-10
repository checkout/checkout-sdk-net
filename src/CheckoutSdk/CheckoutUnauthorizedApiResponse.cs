using System.Net;
using System.Net.Http.Headers;

namespace Checkout
{
    /// <summary>
    /// Checkout API HTTP response message following a HTTP 401 (Unauthorized) response.
    /// </summary>
    public class CheckoutUnauthorizedApiResponse : CheckoutApiHttpResponseMessage
    {
        /// <summary>
        /// Creates a new <see cref="CheckoutUnauthorizedApiResponse"/> instance.
        /// </summary>
        /// <param name="httpResponseHeaders">The headers of the API response.</param>
        public CheckoutUnauthorizedApiResponse(HttpResponseHeaders httpResponseHeaders = null) 
            : base(HttpStatusCode.Unauthorized, httpResponseHeaders) { }
    }
}

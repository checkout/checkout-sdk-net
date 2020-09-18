using System.Net;
using System.Net.Http.Headers;

namespace Checkout
{
    /// <summary>
    /// Checkout API HTTP response message following a HTTP 403 (Forbidden) response.
    /// </summary>
    public class CheckoutForbiddenApiResponse : CheckoutApiHttpResponseMessage
    {
        /// <summary>
        /// Creates a new <see cref="CheckoutForbiddenApiResponse"/> instance.
        /// </summary>
        /// <param name="httpResponseHeaders">The headers of the API response.</param>
        public CheckoutForbiddenApiResponse(HttpResponseHeaders httpResponseHeaders = null) 
            : base(HttpStatusCode.Forbidden, httpResponseHeaders) { }
    }
}

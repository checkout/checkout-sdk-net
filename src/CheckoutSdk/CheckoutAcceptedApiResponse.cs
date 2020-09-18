using System.Net;
using System.Net.Http.Headers;

namespace Checkout
{
    /// <summary>
    /// Checkout API HTTP response message following a HTTP 202 (Accepted) response.
    /// </summary>
    public class CheckoutAcceptedApiResponse : CheckoutApiHttpResponseMessage
    {
        /// <summary>
        /// Creates a new <see cref="CheckoutAcceptedApiResponse"/> instance.
        /// </summary>
        /// <param name="httpResponseHeaders">The headers of the API response.</param>
        public CheckoutAcceptedApiResponse(HttpResponseHeaders httpResponseHeaders = null) 
            : base(HttpStatusCode.Accepted, httpResponseHeaders) { }
    }
}

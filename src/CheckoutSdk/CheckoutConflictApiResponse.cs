using System.Net;
using System.Net.Http.Headers;

namespace Checkout
{
    /// <summary>
    /// Checkout API HTTP response message following a HTTP 409 (Conflict) response.
    /// </summary>
    public class CheckoutConflictApiResponse : CheckoutApiHttpResponseMessage
    {
        /// <summary>
        /// Creates a new <see cref="CheckoutConflictApiResponse"/> instance.
        /// </summary>
        /// <param name="httpResponseHeaders">The headers of the API response.</param>
        public CheckoutConflictApiResponse(HttpResponseHeaders httpResponseHeaders = null) 
            : base(HttpStatusCode.Conflict, httpResponseHeaders) { }
    }
}

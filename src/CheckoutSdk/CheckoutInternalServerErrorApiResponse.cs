using System.Net;
using System.Net.Http.Headers;

namespace Checkout
{
    /// <summary>
    /// Checkout API HTTP response message following a HTTP 500 (Internal Server Error) response.
    /// </summary>
    public class CheckoutInternalServerErrorApiResponse : CheckoutApiHttpResponseMessage
    {
        /// <summary>
        /// Creates a new <see cref="CheckoutInternalServerErrorApiResponse"/> instance.
        /// </summary>
        /// <param name="httpResponseHeaders">The headers of the API response.</param>
        public CheckoutInternalServerErrorApiResponse(HttpResponseHeaders httpResponseHeaders = null) 
            : base(HttpStatusCode.InternalServerError, httpResponseHeaders) { }
    }
}

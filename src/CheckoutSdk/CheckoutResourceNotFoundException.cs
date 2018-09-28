using System.Net;

namespace Checkout
{
    /// <summary>
    /// Exception thrown following a HTTP 404 (Not Found) response.
    /// </summary>
    public class CheckoutResourceNotFoundException : CheckoutApiException
    {
        /// <summary>
        /// Createa a new <see cref="CheckoutResourceNotFoundException"/> instance.
        /// </summary>
        /// <param name="requestId">The unique identifier of the API request.</param>
        public CheckoutResourceNotFoundException(string requestId) 
            : base(HttpStatusCode.NotFound, requestId)
        {
        }
    }
}
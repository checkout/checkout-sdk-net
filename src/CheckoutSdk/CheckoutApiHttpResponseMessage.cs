using System.Net;
using System.Net.Http.Headers;

namespace Checkout
{
    /// <summary>
    /// ApiHttpResponseMessage type for HTTP responses resulting from API operations.
    /// </summary>
    public class CheckoutApiHttpResponseMessage : CheckoutHttpResponseMessage
    {
        /// <summary>
        /// Creates a new <see cref="CheckoutApiHttpResponseMessage"/> instance.
        /// </summary>
        /// <param name="httpStatusCode">The HTTP status code of the API response.</param>
        /// <param name="httpResponseHeaders">The headers of the API response.</param>
        public CheckoutApiHttpResponseMessage(HttpStatusCode httpStatusCode, HttpResponseHeaders httpResponseHeaders = null) 
            : base(httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
            if(httpResponseHeaders != null)
            {
                foreach (var httpResponseHeader in httpResponseHeaders)
                {
                    Headers.Add(httpResponseHeader.Key, httpResponseHeader.Value);
                };
            }
        }

        /// <summary>
        /// Gets the HTTP status code of the API response.
        /// </summary>
        /// <value></value>
        public HttpStatusCode HttpStatusCode { get; }
        
        /// <summary>
        /// Gets the unique identifier of the API request.
        /// </summary>
        public string RequestId { get; }
    }
}

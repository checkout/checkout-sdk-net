using System.Net;
using System.Net.Http;

namespace Checkout
{
    /// <summary>
    /// Base class for HTTP response messages received by the Checkout.com SDK for .NET.
    /// </summary>
    public class CheckoutHttpResponseMessage : HttpResponseMessage
    {
        /// <summary>
        /// Creates a new <see cref="CheckoutHttpResponseMessage"/> instance with the provided HTTP status code.
        /// </summary>
        /// <param name="httpStatusCode">The HTTP status code of the API response.</param>
        public CheckoutHttpResponseMessage(HttpStatusCode httpStatusCode)
            : base(httpStatusCode) { }
    }
}

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Checkout
{
    /// <summary>
    /// Base class for HTTP response messages received by the Checkout.com SDK for .NET.
    /// </summary>
    public class CheckoutHttpResponseMessage<TContentType> : HttpResponseMessage
    {
        /// <summary>
        /// Creates a new <see cref="CheckoutHttpResponseMessage"/> instance with the provided HTTP status code, headers and content.
        /// </summary>
        /// <param name="statusCode">The <see cref="HttpStatusCode"/> of the API response.</param>
        /// <param name="headers">The <see cref="HttpResponseHeaders"/> of the API response.</param>
        /// <param name="content">The deserialized <see cref="Content"/> of the API response.</param>
        public CheckoutHttpResponseMessage(HttpStatusCode statusCode, HttpResponseHeaders headers, TContentType content)
            : this(statusCode, content)
        {
            foreach(var header in headers)
            {
                Headers.Add(header.Key, header.Value);
            }
        }

        /// <summary>
        /// Creates a new <see cref="CheckoutHttpResponseMessage"/> instance with the provided HTTP status code and content.
        /// </summary>
        /// <param name="statusCode">The <see cref="HttpStatusCode"/> of the API response.</param>
        /// <param name="content">The deserialized <see cref="Content"/> of the API response.</param>
        public CheckoutHttpResponseMessage(HttpStatusCode statusCode, TContentType content)
            : this(statusCode)
        {
            Content = content;
        }

        /// <summary>
        /// Creates a new <see cref="CheckoutHttpResponseMessage"/> instance with the provided HTTP status code.
        /// </summary>
        /// <param name="statusCode">The <see cref="HttpStatusCode"/> of the API response.</param>
        public CheckoutHttpResponseMessage(HttpStatusCode statusCode) 
            : base(statusCode) { }

        /// <summary>
        /// Is the deserialized <see cref="Content"/> of the API response.
        /// </summary>
        public new TContentType Content { get; private set; }
    }
}

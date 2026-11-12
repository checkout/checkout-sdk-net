using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;

namespace Checkout
{
    /// <summary>
    /// Thrown when the API returns a status code that does not indicate success, or when a response cannot be read.
    /// </summary>
    [Serializable]
    public sealed class CheckoutApiException : CheckoutException
    {
        /// <summary>
        /// The value of the Cko-Request-Id response header, or null when absent.
        /// </summary>
        public string RequestId { get; }
        /// <summary>
        /// The HTTP status code of the response.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; }
        /// <summary>
        /// The error details parsed from the response body.
        /// </summary>
        public IDictionary<string, object> ErrorDetails { get; }

        /// <summary>
        /// Creates an exception with a message that states the status code.
        /// </summary>
        /// <param name="requestId">The value of the Cko-Request-Id response header.</param>
        /// <param name="httpStatusCode">The HTTP status code of the response.</param>
        /// <param name="errorDetails">The error details parsed from the response body.</param>
        public CheckoutApiException(
            string requestId,
            HttpStatusCode httpStatusCode,
            IDictionary<string, object> errorDetails) :
            base($"The API response status code ({(int)httpStatusCode}) does not indicate success.")
        {
            RequestId = requestId;
            HttpStatusCode = httpStatusCode;
            ErrorDetails = errorDetails;
        }

        /// <summary>
        /// Creates an exception with a custom message and an inner exception.
        /// </summary>
        /// <param name="requestId">The value of the Cko-Request-Id response header.</param>
        /// <param name="httpStatusCode">The HTTP status code of the response.</param>
        /// <param name="errorDetails">The error details parsed from the response body.</param>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The exception that caused this one; may be null.</param>
        public CheckoutApiException(
            string requestId,
            HttpStatusCode httpStatusCode,
            IDictionary<string, object> errorDetails,
            string message,
            Exception innerException) :
            base(message, innerException)
        {
            RequestId = requestId;
            HttpStatusCode = httpStatusCode;
            ErrorDetails = errorDetails;
        }

        private CheckoutApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
using System.Net;
using Checkout.Common;

namespace Checkout
{
    /// <summary>
    /// Exception thrown following a HTTP 422 (Unprocessable) response.
    /// </summary>
    public class CheckoutValidationException : CheckoutApiException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="error">The validation error details.</param>
        /// <param name="httpStatusCode">The HTTP status code of the API response.</param>
        /// <param name="requestId">The unique identifier of the API request.</param>
        public CheckoutValidationException(ErrorResponse error, HttpStatusCode httpStatusCode, string requestId)
         : base(httpStatusCode, requestId, GenerateDetailsMessage(error))
        {
            Error = error;
        }

        /// <summary>
        /// Gets the error response.
        /// </summary>
        public ErrorResponse Error { get; }

        private static string GenerateDetailsMessage(ErrorResponse error)
        {
            return $"A validation error of type {error.ErrorType} occurred with error codes [{string.Join(",", error.ErrorCodes)}].";
        }
    }
}
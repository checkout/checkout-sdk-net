using System.Net;

namespace Checkout
{
    /// <summary>
    /// Exception type for errors resulting from API operations.
    /// </summary>
    public class CheckoutApiException : CheckoutException
    {
        /// <summary>
        /// Creates a new <see cref="CheckoutApiException"/> instance.
        /// </summary>
        /// <param name="httpStatusCode">The HTTP status code of the API response.</param>
        /// <param name="requestId">The unique identifier of the API request.</param>
        /// <param name="additionalInformation">Additional details about the error.</param>
        public CheckoutApiException(HttpStatusCode httpStatusCode, string requestId, string additionalInformation = null) 
            : base(GenerateMessage(httpStatusCode, additionalInformation))
        {
            HttpStatusCode = httpStatusCode;
            RequestId = requestId;
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

        private static string GenerateMessage(HttpStatusCode httpStatusCode, string additionalInformation = null)
        {
            var message = $"The API response status code ({httpStatusCode}) does not indicate success.";
            
            if (!string.IsNullOrWhiteSpace(additionalInformation))
                return message + " " + additionalInformation;
            
            return message;
        }
    }
}
using System.Net;

namespace Checkout
{
    public class CheckoutApiException : CheckoutException
    {
        public CheckoutApiException(HttpStatusCode statusCode, string requestId, string details = null) 
            : base(GenerateMessage(statusCode, details))
        {
            HttpStatusCode = statusCode;
            RequestId = requestId;
        }

        public HttpStatusCode HttpStatusCode { get; }
        public string RequestId { get; }

        private static string GenerateMessage(HttpStatusCode statusCode, string details = null)
            => $"API response status code ({statusCode}) does not indicate success.{(details != null ? " " + details : string.Empty)}";
    }
}
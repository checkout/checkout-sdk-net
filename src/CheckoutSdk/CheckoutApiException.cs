using System.Net;

namespace Checkout.Sdk
{
    public class CheckoutApiException : CheckoutException
    {
        public CheckoutApiException(HttpStatusCode statusCode, string requestId) : base(GenerateMessage(statusCode))
        {
            HttpStatusCode = statusCode;
            RequestId = requestId;
        }

        public HttpStatusCode HttpStatusCode { get; }
        public string RequestId { get; }

        private static string GenerateMessage(HttpStatusCode statusCode)
            => $"API response status code ({statusCode}) does not indicate success";
    }
}
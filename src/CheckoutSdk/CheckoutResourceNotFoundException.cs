using System.Net;

namespace Checkout.Sdk
{
    public class CheckoutResourceNotFoundException : CheckoutApiException
    {
        public CheckoutResourceNotFoundException(HttpStatusCode statusCode, string requestId) 
            : base(statusCode, requestId)
        {
        }
    }
}
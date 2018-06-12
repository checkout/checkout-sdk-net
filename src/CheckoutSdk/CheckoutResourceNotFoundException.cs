using System.Net;

namespace Checkout
{
    public class CheckoutResourceNotFoundException : CheckoutApiException
    {
        public CheckoutResourceNotFoundException(HttpStatusCode statusCode, string requestId) 
            : base(statusCode, requestId)
        {
        }
    }
}
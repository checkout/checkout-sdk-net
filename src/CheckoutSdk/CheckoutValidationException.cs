using System.Net;
using Checkout.Common;

namespace Checkout
{
    public class CheckoutValidationException : CheckoutApiException
    {
        public CheckoutValidationException(ErrorResponse error, HttpStatusCode statusCode, string requestId)
         : base(statusCode, requestId)
        {
            Error = error;
        }

        public ErrorResponse Error { get; }
    }
}
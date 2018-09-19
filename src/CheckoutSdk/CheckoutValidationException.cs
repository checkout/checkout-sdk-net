using System.Net;
using Checkout.Common;

namespace Checkout
{
    public class CheckoutValidationException : CheckoutApiException
    {
        public CheckoutValidationException(ErrorResponse error, HttpStatusCode statusCode, string requestId)
         : base(statusCode, requestId, GenerateDetailsMessage(error))
        {
            Error = error;
        }

        public ErrorResponse Error { get; }

        private static string GenerateDetailsMessage(ErrorResponse error)
        {
            return $"ErrorType: {error.ErrorType} Codes: {string.Join(",", error.ErrorCodes)}";
        }
    }
}
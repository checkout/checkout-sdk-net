using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;

namespace Checkout
{
    [Serializable]
    public sealed class CheckoutApiException : CheckoutException
    {
        public string RequestId { get; }
        public HttpStatusCode HttpStatusCode { get; }
        public IDictionary<string, object> ErrorDetails { get; }

        public CheckoutApiException(
            string requestId,
            HttpStatusCode httpStatusCode,
            IDictionary<string, object> errorDetails) :
            base($"The API response status code ({(int) httpStatusCode}) does not indicate success.")
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
using System.Collections.Generic;

namespace Checkout.Sdk.Common
{
    public class ErrorResponse
    {
        public string RequestId { get; set; }
        public string ErrorType { get; set; }
        public IEnumerable<string> ErrorCodes { get; set; }
    }
}
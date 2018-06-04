using System.Collections.Generic;

namespace Checkout
{
    public class Error
    {
        public string RequestId { get; set; }
        public string ErrorType { get; set; }
        public IEnumerable<string> ErrorCodes { get; set; }
    }
}
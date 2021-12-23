using System.Collections.Generic;

namespace Checkout.Workflows.Four.Reflows
{
    public class ReflowResponse
    {
        public string RequestId { get; set; }

        public string ErrorType { get; set; }

        public IList<string> ErrorCodes { get; set; }
    }
}
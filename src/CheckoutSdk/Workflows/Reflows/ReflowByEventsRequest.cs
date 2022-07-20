using System.Collections.Generic;

namespace Checkout.Workflows.Reflows
{
    public class ReflowByEventsRequest : ReflowRequest
    {
        public IList<string> Events { get; set; }
    }
}
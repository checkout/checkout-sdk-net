using System.Collections.Generic;

namespace Checkout.Workflows.Four.Reflows
{
    public class ReflowByEventsRequest : ReflowRequest
    {
        public IList<string> Events { get; }

        public ReflowByEventsRequest(IList<string> events, IList<string> workflows) : base(workflows)
        {
            this.Events = events;
        }
    }
}
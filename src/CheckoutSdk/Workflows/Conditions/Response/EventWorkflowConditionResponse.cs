using System.Collections.Generic;

namespace Checkout.Workflows.Conditions.Response
{
    public class EventWorkflowConditionResponse : WorkflowConditionResponse
    {
        public IDictionary<string, ISet<string>> Events { get; set; }
    }
}
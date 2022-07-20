using System.Collections.Generic;

namespace Checkout.Workflows.Conditions.Request
{
    public class EventWorkflowConditionRequest : WorkflowConditionRequest
    {
        public IDictionary<string, ISet<string>> Events { get; set; }

        public EventWorkflowConditionRequest() : base(WorkflowConditionType.Event)
        {
        }
    }
}
using System.Collections.Generic;

namespace Checkout.Workflows.Four.Conditions.Request
{
    public class EventWorkflowConditionRequest : WorkflowConditionRequest
    {
        public IDictionary<string, ISet<string>> Events { get; set; }

        public EventWorkflowConditionRequest() : base(WorkflowConditionType.Event)
        {
        }
    }
}
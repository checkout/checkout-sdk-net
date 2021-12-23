using System.Collections.Generic;

namespace Checkout.Workflows.Four.Conditions.Request
{
    public class EventWorkflowConditionRequest : WorkflowConditionRequest
    {
        public IDictionary<string, ISet<string>> Events { get; }

        public EventWorkflowConditionRequest() : base(WorkflowConditionType.Event)
        {
        }

        public EventWorkflowConditionRequest(IDictionary<string, ISet<string>> events) : base(WorkflowConditionType.Event)
        {
            this.Events = events;
        }
    }
}
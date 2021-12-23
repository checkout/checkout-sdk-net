using System.Collections.Generic;

namespace Checkout.Workflows.Four.Conditions.Response
{
    public class EventWorkflowConditionResponse : WorkflowConditionResponse
    {
        public IDictionary<string, ISet<string>> Events { get; set; }

        public EventWorkflowConditionResponse() : base(WorkflowConditionType.Event)
        {
        }
    }
}
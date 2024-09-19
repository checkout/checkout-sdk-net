using Checkout.Common;
using Checkout.Workflows.Actions;

namespace Checkout.Workflows.Events
{
    public class EventActionInvocation : Resource
    {
        public string WorkflowId { get; set; }

        public string WorkflowActionId { get; set; }

        public WorkflowActionStatus? Status { get; set; }
    }
}
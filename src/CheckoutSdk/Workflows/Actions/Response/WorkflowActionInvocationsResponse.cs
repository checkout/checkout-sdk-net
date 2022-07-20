using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Workflows.Actions.Response
{
    public class WorkflowActionInvocationsResponse : Resource
    {
        public string WorkflowId { get; set; }

        public string EventId { get; set; }

        public string WorkflowActionId { get; set; }

        public WorkflowActionType ActionType { get; set; }

        public WorkflowActionStatus Status { get; set; }

        public IList<WorkflowActionInvocation> ActionInvocations { get; set; }
    }
}
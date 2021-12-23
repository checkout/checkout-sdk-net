using Checkout.Workflows.Four.Actions.Request;
using Checkout.Workflows.Four.Conditions.Request;
using System.Collections.Generic;

namespace Checkout.Workflows.Four
{
    public class CreateWorkflowRequest
    {
        public string Name { get; set; }

        public IList<WorkflowConditionRequest> Conditions { get; set; }

        public IList<WorkflowActionRequest> Actions { get; set; }
    }
}
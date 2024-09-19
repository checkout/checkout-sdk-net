using Checkout.Workflows.Actions.Request;
using Checkout.Workflows.Conditions.Request;
using System.Collections.Generic;

namespace Checkout.Workflows
{
    public class UpdateWorkflowRequest
    {
        public string Name { get; set; }
        
        public bool? Active { get; set; }
        
        public IList<WorkflowConditionRequest> Conditions { get; set; }

        public IList<WorkflowActionRequest> Actions { get; set; }
    }
}
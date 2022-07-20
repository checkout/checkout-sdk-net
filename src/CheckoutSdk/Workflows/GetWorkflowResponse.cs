using Checkout.Common;
using Checkout.Workflows.Actions.Response;
using Checkout.Workflows.Conditions.Response;
using System.Collections.Generic;

namespace Checkout.Workflows
{
    public class GetWorkflowResponse : Resource
    {
        public string Id { get; set; }

        public string Name { get; set; }
        
        public bool? Active { get; set; }

        public IList<WorkflowConditionResponse> Conditions { get; set; } = new List<WorkflowConditionResponse>();

        public IList<WorkflowActionResponse> Actions { get; set; } = new List<WorkflowActionResponse>();
    }
}
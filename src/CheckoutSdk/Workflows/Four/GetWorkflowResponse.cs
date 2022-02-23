using Checkout.Common;
using Checkout.Workflows.Four.Actions.Response;
using Checkout.Workflows.Four.Conditions.Response;
using System.Collections.Generic;

namespace Checkout.Workflows.Four
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
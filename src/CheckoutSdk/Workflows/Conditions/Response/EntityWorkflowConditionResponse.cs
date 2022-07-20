using System.Collections.Generic;

namespace Checkout.Workflows.Conditions.Response
{
    public class EntityWorkflowConditionResponse : WorkflowConditionResponse
    {
        public IList<string> Entities { get; set; }
    }
}
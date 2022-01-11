using System.Collections.Generic;

namespace Checkout.Workflows.Four.Conditions.Response
{
    public class EntityWorkflowConditionResponse : WorkflowConditionResponse
    {
        public IList<string> Entities { get; set; }
    }
}
using System.Collections.Generic;

namespace Checkout.Workflows.Four.Conditions.Request
{
    public class EntityWorkflowConditionRequest : WorkflowConditionRequest
    {
        public IList<string> Entities { get; set; }

        public EntityWorkflowConditionRequest() : base(WorkflowConditionType.Entity)
        {
        }
    }
}
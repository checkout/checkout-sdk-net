using System.Collections.Generic;

namespace Checkout.Workflows.Four.Conditions.Request
{
    public class EntityWorkflowConditionRequest : WorkflowConditionRequest
    {
        public IList<string> Entities { get; }

        public EntityWorkflowConditionRequest() : base(WorkflowConditionType.Entity)
        {
        }

        public EntityWorkflowConditionRequest(IList<string> entities) : base(WorkflowConditionType.Entity)
        {
            this.Entities = entities;
        }
    }
}
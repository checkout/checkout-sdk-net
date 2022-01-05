using Checkout.Common;

namespace Checkout.Workflows.Four.Conditions.Response
{
    public class WorkflowConditionResponse : Resource
    {
        public readonly WorkflowConditionType Type;

        public string Id { get; set; }

        public WorkflowConditionResponse(WorkflowConditionType type)
        {
            this.Type = type;
        }
    }
}
using Checkout.Common;

namespace Checkout.Workflows.Four.Conditions.Response
{
    public class WorkflowConditionResponse : Resource
    {
        public WorkflowConditionType Type { get; set; }

        public string Id { get; set; }
    }
}
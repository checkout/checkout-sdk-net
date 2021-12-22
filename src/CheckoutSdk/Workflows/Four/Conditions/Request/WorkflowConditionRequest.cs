namespace Checkout.Workflows.Four.Conditions.Request
{
    public abstract class WorkflowConditionRequest
    {
        public WorkflowConditionType Type { get; }

        public WorkflowConditionRequest(WorkflowConditionType type)
        {
            this.Type = type;
        }
    }
}
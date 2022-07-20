namespace Checkout.Workflows.Conditions.Request
{
    public abstract class WorkflowConditionRequest
    {
        public WorkflowConditionType Type { get; set; }

        protected WorkflowConditionRequest(WorkflowConditionType type)
        {
            Type = type;
        }
    }
}
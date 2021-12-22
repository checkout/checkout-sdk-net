namespace Checkout.Workflows.Four.Actions.Request
{
    public abstract class WorkflowActionRequest
    {
        public WorkflowActionType Type { get; }

        protected internal WorkflowActionRequest(WorkflowActionType type)
        {
            this.Type = type;
        }
    }
}
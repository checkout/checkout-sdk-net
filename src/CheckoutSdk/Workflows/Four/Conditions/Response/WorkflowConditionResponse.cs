namespace Checkout.Workflows.Four.Conditions.Response
{
    public class WorkflowConditionResponse
    {
        public readonly WorkflowConditionType Type;

        public string Id { get; set; }

        public WorkflowConditionResponse(WorkflowConditionType type)
        {
            this.Type = type;
        }
    }
}
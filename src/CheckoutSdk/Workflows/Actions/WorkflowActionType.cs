using System.Runtime.Serialization;

namespace Checkout.Workflows.Actions
{
    public enum WorkflowActionType
    {
        [EnumMember(Value = "webhook")] Webhook
    }
}
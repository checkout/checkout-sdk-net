using System.Runtime.Serialization;

namespace Checkout.Workflows.Four.Actions
{
    public enum WorkflowActionType
    {
        [EnumMember(Value = "webhook")]
        Webhook
    }
}
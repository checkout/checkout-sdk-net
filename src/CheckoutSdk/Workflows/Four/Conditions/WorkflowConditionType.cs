using System.Runtime.Serialization;

namespace Checkout.Workflows.Four.Conditions
{
    public enum WorkflowConditionType
    {
        [EnumMember(Value = "event")] Event,
        [EnumMember(Value = "entity")] Entity,
        [EnumMember(Value = "processing_channel")] ProcessingChannel
    }
}
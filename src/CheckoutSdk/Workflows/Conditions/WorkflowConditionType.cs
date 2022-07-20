using System.Runtime.Serialization;

namespace Checkout.Workflows.Conditions
{
    public enum WorkflowConditionType
    {
        [EnumMember(Value = "event")] Event,
        [EnumMember(Value = "entity")] Entity,

        [EnumMember(Value = "processing_channel")]
        ProcessingChannel
    }
}
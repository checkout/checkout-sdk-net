using System.Runtime.Serialization;

namespace Checkout.Workflows.Four
{
    public enum WorkflowConditionType
    {
        [EnumMember(Value = "event")] Event,
        [EnumMember(Value = "entity")] Entity
    }
}
using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Requirements
{
    public enum EntityRequirementPriority
    {
        [EnumMember(Value = "high")]
        High,

        [EnumMember(Value = "critical")]
        Critical
    }
}

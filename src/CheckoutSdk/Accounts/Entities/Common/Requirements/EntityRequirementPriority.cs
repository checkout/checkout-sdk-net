using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Requirements
{
    public enum EntityRequirementPriority
    {
        [EnumMember(Value = "high")]
        High,

        [EnumMember(Value = "critical")]
        Critical
    }
}

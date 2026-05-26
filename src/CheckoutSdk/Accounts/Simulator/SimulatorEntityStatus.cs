using System.Runtime.Serialization;

namespace Checkout.Accounts.Simulator
{
    public enum SimulatorEntityStatus
    {
        [EnumMember(Value = "draft")]
        Draft,

        [EnumMember(Value = "requirements_due")]
        RequirementsDue,

        [EnumMember(Value = "pending")]
        Pending,

        [EnumMember(Value = "active")]
        Active,

        [EnumMember(Value = "restricted")]
        Restricted,

        [EnumMember(Value = "rejected")]
        Rejected,

        [EnumMember(Value = "inactive")]
        Inactive
    }
}

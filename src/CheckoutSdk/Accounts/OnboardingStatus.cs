using System.Runtime.Serialization;

namespace Checkout.Accounts
{
    public enum OnboardingStatus
    {
        [EnumMember(Value = "active")]
        Active,

        [EnumMember(Value = "pending")]
        Pending,

        [EnumMember(Value = "restricted")]
        Restricted,

        [EnumMember(Value = "requirements_due")]
        RequirementsDue,

        [EnumMember(Value = "inactive")]
        Inactive,

        [EnumMember(Value = "rejected")]
        Rejected
    }
}
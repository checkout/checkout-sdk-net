using System.Runtime.Serialization;

namespace Checkout.Issuing.Cardholders.Responses
{
    public enum CardholderStatus
    {
        [EnumMember(Value = "active")] Active,
        [EnumMember(Value = "pending")] Pending,
        [EnumMember(Value = "restricted")] Restricted,
        [EnumMember(Value = "requirements_due")] RequirementsDue,
        [EnumMember(Value = "inactive")] Inactive,
        [EnumMember(Value = "rejected")] Rejected
    }
}
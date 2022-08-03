using System.Runtime.Serialization;

namespace Checkout.Accounts
{
    public enum BusinessType
    {
        [EnumMember(Value = "general_partnership")] GeneralPartnership,
        [EnumMember(Value = "limited_partnership")] LimitedPartnership,
        [EnumMember(Value = "public_limited_company")] PublicLimitedCompany,
        [EnumMember(Value = "limited_company")] LimitedCompany,
        [EnumMember(Value = "professional_association")] ProfessionalAssociation,
        [EnumMember(Value = "unincorporated_association")] UnincorporatedAssociation,
        [EnumMember(Value = "auto_entrepreneur")] AutoEntrepreneur,
    }
}
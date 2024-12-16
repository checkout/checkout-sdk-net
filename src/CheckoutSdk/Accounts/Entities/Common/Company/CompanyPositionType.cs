using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Company
{
    public enum CompanyPositionType
    {
        [EnumMember(Value = "ceo")]
        CEO,
        
        [EnumMember(Value = "cfo")]
        CFO,
        
        [EnumMember(Value = "coo")]
        COO,

        [EnumMember(Value = "managing_member")]
        ManagingMember,

        [EnumMember(Value = "general_partner")]
        GeneralPartner,
        
        [EnumMember(Value = "president")]
        President,
        
        [EnumMember(Value = "vice_president")]
        VicePresident,
        
        [EnumMember(Value = "treasurer")]
        Treasurer,

        [EnumMember(Value = "other_senior_management")]
        OtherSeniorManagement,

        [EnumMember(Value = "other_executive_officer")]
        OtherExecutiveOfficer,

        [EnumMember(Value = "other_non_executive_non_senior")]
        OtherNonExecutiveNonSenior
    }
}
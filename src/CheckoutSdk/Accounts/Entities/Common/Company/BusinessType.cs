using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Company
{
    public enum BusinessType
    {
        // GB Company Full (3.0) Business Types
        [EnumMember(Value = "scottish_limited_partnership")]
        ScottishLimitedPartnership,
        
        // Common
        [EnumMember(Value = "unincorporated_association")]
        UnincorporatedAssociation,
        
        [EnumMember(Value = "private_corporation")]
        PrivateCorporation,

        [EnumMember(Value = "limited_liability_corporation")]
        LimitedLiabilityCorporation,

        [EnumMember(Value = "publicly_traded_corporation")]
        PubliclyTradedCorporation,

        [EnumMember(Value = "regulated_financial_institution")]
        RegulatedFinancialInstitution,

        [EnumMember(Value = "sec_registered_entity")]
        SecRegisteredEntity,

        [EnumMember(Value = "cftc_registered_entity")]
        CftcRegisteredEntity,
        
        [EnumMember(Value = "individual_or_sole_proprietorship")]
        IndividualOrSoleProprietorship,
        
        [EnumMember(Value = "government_agency")]
        GovernmentAgency,
        
        [EnumMember(Value = "non_profit_entity")]
        NonProfitEntity,
        
        [EnumMember(Value = "trust")]
        Trust,
        
        [EnumMember(Value = "club_or_society")]
        ClubOrSociety,
        
        [EnumMember(Value = "general_partnership")]
        GeneralPartnership,
        
        [EnumMember(Value = "limited_partnership")]
        LimitedPartnership,
        
        [EnumMember(Value = "public_limited_company")]
        PublicLimitedCompany,
        
        [EnumMember(Value = "limited_company")]
        LimitedCompany,
        
        [EnumMember(Value = "professional_association")]
        ProfessionalAssociation,
        
        [EnumMember(Value = "auto_entrepreneur")]
        AutoEntrepreneur,
    }
}
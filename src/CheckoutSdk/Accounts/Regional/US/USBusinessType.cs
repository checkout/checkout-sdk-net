using System.Runtime.Serialization;

namespace Checkout.Accounts.Regional.US
{
    public enum USBusinessType
    {
        [EnumMember(Value = "private_corporation")] PrivateCorporation,
        [EnumMember(Value = "publicly_traded_corporation")] PubliclyTradedCorporation,
        [EnumMember(Value = "government_agency")] GovernmentAgency,
        [EnumMember(Value = "individual_or_sole_proprietorship")] IndividualOrSoleProprietorship,
        [EnumMember(Value = "limited_liability_corporation")] LimitedLiabilityCorporation,
        [EnumMember(Value = "limited_partnership")] LimitedPartnership,
        [EnumMember(Value = "non_profit_entity")] NonProfitEntity,
        [EnumMember(Value = "sec_registered_entity")] SecRegisteredEntity,
        [EnumMember(Value = "cftc_registered_entity")] CftcRegisteredEntity,
        [EnumMember(Value = "regulated_financial_institution")] RegulatedFinancialInstitution,
    }
}
using System.Runtime.Serialization;

namespace Checkout.Common.Four
{
    public enum AccountHolderIdentificationType
    {
        [EnumMember(Value = "passport")] Passport,
        [EnumMember(Value = "driving_licence")] DrivingLicence,
        [EnumMember(Value = "national_id")] NationalId,
        [EnumMember(Value = "company_registration")] CompanyRegistration,
        [EnumMember(Value = "tax_id")] TaxId
    }
}
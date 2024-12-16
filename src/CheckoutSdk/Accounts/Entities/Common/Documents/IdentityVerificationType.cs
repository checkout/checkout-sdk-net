using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Documents
{
    public enum IdentityVerificationType
    {
        [EnumMember(Value = "passport")]
        Passport,

        [EnumMember(Value = "national_identity_card")]
        NationalIdentityCard,

        [EnumMember(Value = "driving_license")]
        DrivingLicense,

        [EnumMember(Value = "citizen_card")]
        CitizenCard,

        [EnumMember(Value = "residence_permit")]
        ResidencePermit,

        [EnumMember(Value = "electoral_id")]
        ElectoralId,
    }
}
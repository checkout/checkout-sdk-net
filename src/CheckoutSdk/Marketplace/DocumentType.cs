using System.Runtime.Serialization;

namespace Checkout.Marketplace
{
    public enum DocumentType
    {
        [EnumMember(Value = "passport")]
        Passport,

        [EnumMember(Value = "national_identity_card")]
        NationalIdentityCard,

        [EnumMember(Value = "driving_license")]
        DrivingLicense,

        [EnumMember(Value = "citizen_card")]
        CitizienCard,

        [EnumMember(Value = "residence_permit")]
        ResidencePermit,

        [EnumMember(Value = "electoral_id")]
        ElectoralId
    }
}
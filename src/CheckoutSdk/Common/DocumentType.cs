using System.Runtime.Serialization;

namespace Checkout.Common
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
        CitizenCard,

        [EnumMember(Value = "residence_permit")]
        ResidencePermit,

        [EnumMember(Value = "electoral_id")] 
        ElectoralId
    }
}
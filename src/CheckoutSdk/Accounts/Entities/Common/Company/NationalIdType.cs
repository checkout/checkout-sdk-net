using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Company
{
    public enum NationalIdType
    {
        [EnumMember(Value = "ssn")]
        Ssn,

        [EnumMember(Value = "itin")]
        Itin,

        [EnumMember(Value = "passport")]
        Passport,

        [EnumMember(Value = "driving_license")]
        DrivingLicense,

        [EnumMember(Value = "national_id_card")]
        NationalIdCard,

        [EnumMember(Value = "residence_permit")]
        ResidencePermit,

        [EnumMember(Value = "other")]
        Other
    }
}

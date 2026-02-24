using System.Runtime.Serialization;

namespace Checkout.Identities.Entities
{
    public enum DocumentType
    {
        [EnumMember(Value = "Driving licence")]
        DrivingLicence,
        [EnumMember(Value = "ID")]
        ID,
        [EnumMember(Value = "Passport")]
        Passport,
        [EnumMember(Value = "Residence Permit")]
        ResidencePermit
    }
}
using System.Runtime.Serialization;

namespace Checkout.Identities.Entities
{
    /// <summary>
    /// The type of identity document.
    /// </summary>
    public enum DocumentType
    {
        [EnumMember(Value = "Driving licence")]
        DrivingLicence,

        [EnumMember(Value = "ID")]
        ID,

        [EnumMember(Value = "Other")]
        Other,

        [EnumMember(Value = "Passport")]
        Passport,

        [EnumMember(Value = "Residence Permit")]
        ResidencePermit,

        [EnumMember(Value = "Travel Document")]
        TravelDocument,

        [EnumMember(Value = "Visa")]
        Visa
    }
}

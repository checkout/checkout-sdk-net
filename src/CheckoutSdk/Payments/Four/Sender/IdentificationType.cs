using System.Runtime.Serialization;

namespace Checkout.Payments.Four.Sender
{
    public enum IdentificationType
    {
        [EnumMember(Value = "passport")] Passport,
        [EnumMember(Value = "driving_licence")] DrivingLicence,
        [EnumMember(Value = "national_id")] NationalId
    }
}
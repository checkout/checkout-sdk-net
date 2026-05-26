using System.Runtime.Serialization;

namespace Checkout.Payments.Setups.Entities
{
    public enum AccountFundingTransactionIdentificationType
    {
        [EnumMember(Value = "passport")]
        Passport,

        [EnumMember(Value = "driving_license")]
        DrivingLicense,

        [EnumMember(Value = "national_id")]
        NationalId
    }
}

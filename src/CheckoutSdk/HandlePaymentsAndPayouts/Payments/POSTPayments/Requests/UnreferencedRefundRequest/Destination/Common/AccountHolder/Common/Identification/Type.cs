using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.Common.AccountHolder.Common.Identification
{
    public enum Type
    {
        [EnumMember(Value = "passport")]
        Passport,

        [EnumMember(Value = "driving_license")]
        DrivingLicense,

        [EnumMember(Value = "national_id")]
        NationalId,

        [EnumMember(Value = "company_registration")]
        CompanyRegistration,

        [EnumMember(Value = "tax_id")]
        TaxId,
    }
}
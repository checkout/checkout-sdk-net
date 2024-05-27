using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum TrustedListingStatus
    {
        [EnumMember(Value = "Y")] ThreeDSRequestorIsAllowedByCardHolder,
        [EnumMember(Value = "N")] ThreeDSRequestorIsNotAllowedByCardHolder,
        [EnumMember(Value = "E")] NotEligibleAsDeterminedByIssuer,
        [EnumMember(Value = "P")] PendingConfirmationByCardholder,
        [EnumMember(Value = "R")] CardholderRejected,
        [EnumMember(Value = "U")] AllowlistStatusUnknownOrUnavailableOrDoesNotApply,
    }
}
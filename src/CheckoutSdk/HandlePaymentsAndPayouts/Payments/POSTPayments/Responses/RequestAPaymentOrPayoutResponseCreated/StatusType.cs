using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated
{
    public enum StatusType
    {
        [EnumMember(Value = "Authorized")]
        Authorized,

        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "Card Verified")]
        CardVerified,

        [EnumMember(Value = "Declined")]
        Declined,

        [EnumMember(Value = "Retry Scheduled")]
        RetryScheduled,
    }
}
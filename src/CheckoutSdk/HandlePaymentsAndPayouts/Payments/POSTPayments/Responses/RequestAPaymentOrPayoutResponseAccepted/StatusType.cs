using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseAccepted
{
    public enum StatusType
    {
        [EnumMember(Value = "Accepted")]
        Accepted,

        [EnumMember(Value = "Rejected")]
        Rejected,

        [EnumMember(Value = "Pending")]
        Pending,
    }
}
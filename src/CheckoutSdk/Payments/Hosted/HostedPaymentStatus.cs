using System.Runtime.Serialization;

namespace Checkout.Payments.Hosted
{
    public enum HostedPaymentStatus
    {
        [EnumMember(Value = "Payment Pending")]
        PaymentPending,

        [EnumMember(Value = "Payment Received")]
        PaymentReceived,

        [EnumMember(Value = "Expired")] Expired
    }
}
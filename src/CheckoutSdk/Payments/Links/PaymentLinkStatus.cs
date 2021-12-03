using System.Runtime.Serialization;

namespace Checkout.Payments.Links
{
    public enum PaymentLinkStatus
    {
        [EnumMember(Value = "Active")] Active,

        [EnumMember(Value = "Payment Received")]
        PaymentReceived,

        [EnumMember(Value = "Expired")] Expired
    }
}
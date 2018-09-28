using System.Runtime.Serialization;

namespace Checkout.Payments
{
    /// <summary>
    /// Defines the status of a payment.
    /// </summary>
    public enum PaymentStatus
    {
        Authorized,
        Cancelled,
        Captured,
        Declined,
        Expired,
        [EnumMember(Value = "Partially Captured")]
        PartiallyCaptured,
        [EnumMember(Value = "Partially Refunded")]
        PartiallyRefunded,
        Pending,
        Refunded,
        Voided,
        [EnumMember(Value = "Card Verified")]
        CardVerified,
        Chargeback
    }
}

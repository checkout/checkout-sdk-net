using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum PaymentStatus
    {
        [EnumMember(Value = "Active")] Active,
        [EnumMember(Value = "Pending")] Pending,
        [EnumMember(Value = "Authorized")] Authorized,
        [EnumMember(Value = "Card Verified")] CardVerified,
        [EnumMember(Value = "Voided")] Voided,

        [EnumMember(Value = "Partially Captured")]
        PartiallyCaptured,
        [EnumMember(Value = "Captured")] Captured,

        [EnumMember(Value = "Partially Refunded")]
        PartiallyRefunded,
        [EnumMember(Value = "Refunded")] Refunded,
        [EnumMember(Value = "Declined")] Declined,
        [EnumMember(Value = "Canceled")] Canceled,
        [EnumMember(Value = "Expired")] Expired,
        [EnumMember(Value = "Requested")] Requested,
        [EnumMember(Value = "Paid")] Paid
    }
}
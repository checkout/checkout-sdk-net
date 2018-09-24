namespace Checkout.Payments
{
    public enum PaymentStatus
    {
        Authorized,
        Cancelled,
        Captured,
        Declined,
        Expired,
        PartiallyCaptured,
        PartiallyRefunded,
        Pending,
        Refunded,
        Voided,
        CardVerified,
        Chargeback
    }
}

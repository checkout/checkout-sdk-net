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
        PartiallyCaptured,
        PartiallyRefunded,
        Pending,
        Refunded,
        Voided,
        CardVerified,
        Chargeback
    }
}

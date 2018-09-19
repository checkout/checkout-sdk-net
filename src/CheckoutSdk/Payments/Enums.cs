namespace Checkout.Payments
{
    public enum ActionType
    {
        Authorization,
        CardVerification,
        Void,
        Capture,
        Refund
    }

    public enum PaymentType
    {
        Regular,
        Recurring,
        Moto
    }

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

using System.Runtime.Serialization;

namespace Checkout.Payments
{
    /// <summary>
    /// Defines the status of a payment.
    /// </summary>
    public static class PaymentStatus
    {
        public const string Authorized = "Authorized";
        public const string Canceled = "Canceled";
        public const string Captured = "Captured";
        public const string Declined = "Declined";
        public const string Expired = "Expired";
        public const string PartiallyCaptured = "Partially Captured";
        public const string PartiallyRefunded = "Partially Refunded";
        public const string Pending = "Pending";
        public const string Refunded = "Refunded";
        public const string Voided = "Voided";
        public const string CardVerified = "Card Verified";
        public const string Chargeback = "Chargeback";
    }
}

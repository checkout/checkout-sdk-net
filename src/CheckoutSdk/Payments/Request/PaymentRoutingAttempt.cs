namespace Checkout.Payments.Request
{
    public class PaymentRoutingAttempt
    {
        /// <summary>
        /// The card scheme to use for the payment attempt.
        /// [Optional]
        /// Enum: "accel" "amex" "cartes_bancaires" "diners" "discover" "jcb" "mada"
        ///       "maestro" "mastercard" "nyce" "omannet" "pulse" "shazam" "star" "upi" "visa"
        /// </summary>
        public PaymentRoutingScheme? Scheme { get; set; }
    }
}

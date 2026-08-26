using Checkout.Payments;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Requests
{
    /// <summary>
    /// Base class for the payment session requests that create a session.
    ///
    /// The properties declared here are accepted only when a payment session is created, either by
    /// Request a Payment Session (POST /payment-sessions) or by Request a Payment Session with
    /// Payment (POST /payment-sessions/complete). They are not accepted by Submit a Payment
    /// Session (POST /payment-sessions/{id}/submit), which is why PaymentSessionSubmitRequest
    /// does not derive from this class.
    ///
    /// The defaults declared here mirror the API defaults, so a request that omits them behaves
    /// the same whether or not the SDK sends them.
    /// </summary>
    public abstract class PaymentSessionCreateBase : PaymentSessionInfo
    {
        /// <summary>
        /// Specifies whether to capture the payment, if applicable. Default: true
        /// </summary>
        public bool? Capture { get; set; } = true;

        /// <summary>
        /// Must be specified for card-not-present (CNP) payments. Default: "Regular"
        /// Enum: "Regular" "Recurring" "MOTO" "Installment" "Unscheduled"
        /// </summary>
        public PaymentType? PaymentType { get; set; } = Checkout.Payments.PaymentType.Regular;

        /// <summary>
        /// Creates a translated version of the page in the specified language. Default: "en-GB"
        /// </summary>
        public LocaleType? Locale { get; set; } = LocaleType.EnGb;

        /// <summary>
        /// The authorization type.
        /// [Optional]
        /// Enum: "Final" "Estimated"
        /// Default: "Final"
        /// </summary>
        public AuthorizationType? AuthorizationType { get; set; }

        /// <summary>
        /// A description for the payment.
        /// max 100 characters
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The merchant's display name.
        /// max 255 characters
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The information to process a recurring payment request. To be used when the payment_type is Recurring.
        /// [Optional]
        /// </summary>
        public PaymentPlan PaymentPlan { get; set; }

        /// <summary>
        /// Configures the risk assessment performed during payment processing.
        /// </summary>
        public RiskRequest Risk { get; set; }
    }
}

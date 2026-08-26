using Checkout.HandlePaymentsAndPayouts.Flow.Entities;

using PaymentType = Checkout.Payments.PaymentType;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Requests
{
    /// <summary>
    /// Request to submit a payment session.
    ///
    /// Every property is optional except SessionData. A property you do not set is omitted from
    /// the request body, which leaves the value provided when the payment session was created
    /// untouched. This is why Capture and PaymentType carry no default here, unlike on the
    /// session creation requests.
    /// </summary>
    public class PaymentSessionSubmitRequest : PaymentSessionInfo
    {
        /// <summary>
        /// A unique token representing the additional customer data captured by Flow,
        /// as received from the handleSubmit callback.
        /// Do not log or store this value.
        /// [Required]
        /// </summary>
        public string SessionData { get; set; }

        /// <summary>
        /// Specifies whether to capture the payment, if applicable.
        /// Leave this property unset to keep the value provided when the payment session was
        /// created. If it was not provided then either, the API applies its default of true.
        /// </summary>
        public bool? Capture { get; set; }

        /// <summary>
        /// Must be specified for card-not-present (CNP) payments.
        /// Leave this property unset to keep the value provided when the payment session was
        /// created. If it was not provided then either, the API applies its default of "Regular".
        /// Enum: "Regular" "Recurring" "MOTO" "Installment" "Unscheduled"
        /// </summary>
        public PaymentType? PaymentType { get; set; }

        /// <summary>
        /// Configurations for payment method-specific settings.
        /// </summary>
        public PaymentMethodConfiguration PaymentMethodConfiguration { get; set; }

        /// <summary>
        /// Deprecated - The Customer's IP address. Only IPv4 and IPv6 addresses are accepted.
        /// </summary>
        [System.Obsolete("ip_address is deprecated. Use billing.address instead.")]
        public string IpAddress { get; set; }
    }
}

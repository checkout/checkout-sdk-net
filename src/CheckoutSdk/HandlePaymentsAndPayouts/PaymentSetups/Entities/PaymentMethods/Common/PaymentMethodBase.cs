using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// Base class for all payment methods with common properties
    /// </summary>
    public abstract class PaymentMethodBase
    {
        /// <summary>
        /// The payment method status.
        /// [Optional]
        /// readOnly
        /// Enum: "unavailable" "action_required" "ready" "initialization_required" "invalid"
        /// </summary>
        public PaymentMethodStatus? Status { get; set; }

        /// <summary>
        /// The list of error codes or indicators that highlight missing or invalid information.
        /// [Optional]
        /// readOnly
        /// </summary>
        public IList<string> Flags { get; set; }

        /// <summary>
        /// The initialization state of the payment method. When you create a Payment Setup, this
        /// defaults to disabled.
        /// [Optional]
        /// Default: "disabled"
        /// Enum: "disabled" "enabled"
        /// </summary>
        public PaymentMethodInitialization Initialization { get; set; } = PaymentMethodInitialization.Disabled;
    }
}
using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// Base class for all payment methods with common properties
    /// </summary>
    public abstract class PaymentMethodBase
    {
        /// <summary>
        /// The status of the payment method
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Configuration flags for the payment method
        /// </summary>
        public IList<string> Flags { get; set; }

        /// <summary>
        /// Default: "disabled"
        /// The initialization state of the payment method.
        /// When you create a Payment Setup, this defaults to disabled.
        /// Enum: "disabled" "enabled"
        /// </summary>
        public PaymentMethodInitialization Initialization { get; set; } = PaymentMethodInitialization.Disabled;
    }
}
using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class Bizum
    {
        /// <summary>
        /// The status of the Bizum payment method
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Configuration flags for the Bizum payment method
        /// </summary>
        public IList<string> Flags { get; set; }

        /// <summary>
        ///  Default: "disabled"
        /// The initialization state of the payment method.
        /// When you create a Payment Setup, this defaults to disabled.
        /// Enum: "disabled" "enabled"
        /// </summary>
        public PaymentMethodInitialization Initialization { get; set; } = PaymentMethodInitialization.Disabled;

        /// <summary>
        /// Payment method options specific to Bizum
        /// </summary>
        public BizumOptions PaymentMethodOptions { get; set; }
    }
}
using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class Stcpay
    {
        /// <summary>
        /// The status of the STC Pay payment method
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Configuration flags for the STC Pay payment method
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
        /// The one-time password (OTP) for STC Pay authentication
        /// </summary>
        public string Otp { get; set; }

        /// <summary>
        /// Payment method options specific to STC Pay
        /// </summary>
        public StcpayOptions PaymentMethodOptions { get; set; }
    }
}
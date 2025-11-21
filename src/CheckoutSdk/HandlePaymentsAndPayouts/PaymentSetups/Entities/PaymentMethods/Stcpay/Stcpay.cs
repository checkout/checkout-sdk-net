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
        /// The initialization status or token for STC Pay
        /// </summary>
        public string Initialization { get; set; }

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
using System;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The stc pay payment method's details and configuration.
    /// </summary>
    public class Stcpay : PaymentMethodBase
    {
        /// <summary>
        /// The one-time password (OTP) for stc pay.
        /// [Optional]
        /// writeOnly
        /// </summary>
        public string Otp { get; set; }

        /// <summary>
        /// The next available action for the payment method. For stc pay the type is "otp".
        /// [Optional]
        /// readOnly
        /// </summary>
        public PaymentMethodAction Action { get; set; }

        /// <summary>
        /// The payment method options.
        /// [Optional]
        /// Deprecated: not part of the API spec; the API no longer sends it and ignores it on requests.
        /// </summary>
        [Obsolete("Not part of the Payment Setups API specification; the API no longer sends it and ignores it on requests. Use Action instead. Will be removed in the next major version.")]
        public PaymentMethodOptions PaymentMethodOptions { get; set; }
    }
}

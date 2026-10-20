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
        /// Payment method options specific to STC Pay
        /// </summary>
        public PaymentMethodOptions PaymentMethodOptions { get; set; }
    }
}

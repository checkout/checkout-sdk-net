namespace Checkout.Payments.Setups.Entities
{
    public class Stcpay : PaymentMethodBase
    {
        /// <summary>
        /// The one-time password (OTP) for STC Pay authentication
        /// </summary>
        public string Otp { get; set; }

        /// <summary>
        /// Payment method options specific to STC Pay
        /// </summary>
        public PaymentMethodOptions PaymentMethodOptions { get; set; }
    }
}
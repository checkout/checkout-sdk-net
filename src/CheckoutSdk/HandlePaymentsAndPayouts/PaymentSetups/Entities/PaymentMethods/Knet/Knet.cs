namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The KNET payment method's details and configuration.
    /// </summary>
    public class Knet : PaymentSetupPaymentMethod
    {
        /// <summary>
        /// The customer's preferred language, as an ISO 639-1 code. For example, the language selected on
        /// your site, if the issuer's site supports it.
        /// [Optional]
        /// </summary>
        public KnetLanguage? Language { get; set; }
    }
}

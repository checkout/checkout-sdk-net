namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The PayPal payment method's details and configuration.
    /// </summary>
    public class Paypal : PaymentMethodBase
    {
        /// <summary>
        /// The user action for the PayPal widget.
        /// - pay_now: The customer is immediately directed to finalize the payment.
        /// - continue: The customer is redirected back to the merchant's site to review and finalize the payment.
        /// [Optional]
        /// Enum: "pay_now" "continue"
        /// </summary>
        public PaypalUserAction? UserAction { get; set; }

        /// <summary>
        /// The brand name to display in the PayPal checkout experience.
        /// [Optional]
        /// &lt;= 127 characters
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// Where to obtain the shipping information.
        /// [Optional]
        /// Enum: "no_shipping" "get_from_file" "set_provided_address"
        /// </summary>
        public PaypalShippingPreference? ShippingPreference { get; set; }

        /// <summary>
        /// The next available action for the PayPal payment method (response only).
        /// [Optional]
        /// </summary>
        public PaymentMethodAction Action { get; set; }
    }
}

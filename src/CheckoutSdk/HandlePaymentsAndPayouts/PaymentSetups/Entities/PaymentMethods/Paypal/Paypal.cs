namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The PayPal payment method's details and configuration.
    /// </summary>
    public class Paypal : PaymentMethodBase
    {
        /// <summary>
        /// The user action for the PayPal widget.
        /// - pay_now: After the customer clicks the PayPal button, the customer is redirected to a page to enter
        /// payment details and immediately directed to finalize the payment.
        /// - continue: After the customer clicks the PayPal button, the customer is redirected to a page to enter
        /// payment details and then redirected back to the merchant's site to review and finalize the payment.
        /// [Optional]
        /// Enum: "pay_now" "continue"
        /// </summary>
        public PaypalUserAction? UserAction { get; set; }

        /// <summary>
        /// The brand name to display in the PayPal checkout experience.
        /// [Optional]
        /// max 127 characters
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// Where to obtain the shipping information.
        /// - no_shipping: Redacts the shipping address from the PayPal pages.
        /// - get_from_file: Obtains the shipping address from the customer's PayPal account.
        /// - set_provided_address: Uses the shipping address provided by the merchant.
        /// [Optional]
        /// Enum: "no_shipping" "get_from_file" "set_provided_address"
        /// </summary>
        public PaypalShippingPreference? ShippingPreference { get; set; }

        /// <summary>
        /// The next available action for the payment method. For PayPal the type is "sdk", and the action
        /// carries the order_id to use with the PayPal SDK.
        /// [Optional]
        /// readOnly
        /// </summary>
        public PaymentMethodAction Action { get; set; }
    }
}

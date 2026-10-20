namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The next available action for a Payment Setup payment method (response only). Shared by Klarna, PayPal and
    /// stc pay (and by the legacy PaymentMethodOption); each method returns only the properties that apply to it.
    /// </summary>
    public class PaymentMethodAction
    {
        /// <summary>
        /// The type of action.
        /// - Klarna: "sdk"
        /// - PayPal: "sdk"
        /// - stc pay: "otp"
        /// [Optional]
        /// readOnly
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The client token for the Klarna SDK (Klarna only).
        /// [Optional]
        /// readOnly
        /// </summary>
        public string ClientToken { get; set; }

        /// <summary>
        /// The session ID (Klarna only).
        /// [Optional]
        /// readOnly
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// The PayPal order ID to use with the PayPal SDK (PayPal only).
        /// [Optional]
        /// readOnly
        /// </summary>
        public string OrderId { get; set; }
    }
}

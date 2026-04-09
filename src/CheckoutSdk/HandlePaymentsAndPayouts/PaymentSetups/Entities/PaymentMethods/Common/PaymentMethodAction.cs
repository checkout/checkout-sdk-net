namespace Checkout.Payments.Setups.Entities
{
    public class PaymentMethodAction
    {
        /// <summary>
        /// The type of action to be performed with the payment method
        /// [Optional]
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The client token for payment method authentication
        /// [Optional]
        /// </summary>
        public string ClientToken { get; set; }

        /// <summary>
        /// The session identifier for the payment method session
        /// [Optional]
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// The PayPal order ID to use with the PayPal SDK (PayPal only).
        /// [Optional]
        /// </summary>
        public string OrderId { get; set; }
    }
}
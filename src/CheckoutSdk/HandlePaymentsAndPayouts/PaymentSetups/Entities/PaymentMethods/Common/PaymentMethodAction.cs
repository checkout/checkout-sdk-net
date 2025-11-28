using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentMethodAction
    {
        /// <summary>
        /// The type of action to be performed with the payment method
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The client token for payment method authentication
        /// </summary>
        public string ClientToken { get; set; }

        /// <summary>
        /// The session identifier for the payment method session
        /// </summary>
        public string SessionId { get; set; }
    }
}
using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentMethodOption
    {
        /// <summary>
        /// The unique identifier for the payment method option
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The status of the payment method option
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Configuration flags for the payment method option
        /// </summary>
        public IList<string> Flags { get; set; }

        /// <summary>
        /// The action configuration for STC Pay full payment
        /// </summary>
        public PaymentMethodAction Action { get; set; }
    }
}
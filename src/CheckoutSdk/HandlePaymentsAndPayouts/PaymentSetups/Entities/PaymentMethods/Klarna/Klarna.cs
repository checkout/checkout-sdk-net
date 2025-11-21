using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class Klarna
    {
        /// <summary>
        /// The status of the Klarna payment method
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Configuration flags for the Klarna payment method
        /// </summary>
        public IList<string> Flags { get; set; }

        /// <summary>
        /// The initialization status or token for Klarna
        /// </summary>
        public string Initialization { get; set; }

        /// <summary>
        /// The account holder information for Klarna payments
        /// </summary>
        public KlarnaAccountHolder AccountHolder { get; set; }

        /// <summary>
        /// Payment method options specific to Klarna
        /// </summary>
        public KlarnaPaymentMethodOptions PaymentMethodOptions { get; set; }
    }
}
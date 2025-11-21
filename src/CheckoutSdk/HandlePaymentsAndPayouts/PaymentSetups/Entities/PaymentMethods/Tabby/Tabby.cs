using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class Tabby
    {
        /// <summary>
        /// The status of the Tabby payment method
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Configuration flags for the Tabby payment method
        /// </summary>
        public IList<string> Flags { get; set; }

        /// <summary>
        /// The initialization status or token for Tabby
        /// </summary>
        public string Initialization { get; set; }

        /// <summary>
        /// Payment method options specific to Tabby
        /// </summary>
        public TabbyOptions PaymentMethodOptions { get; set; }
    }
}
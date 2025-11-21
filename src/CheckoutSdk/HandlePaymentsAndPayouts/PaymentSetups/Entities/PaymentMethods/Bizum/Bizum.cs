using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class Bizum
    {
        /// <summary>
        /// The status of the Bizum payment method
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Configuration flags for the Bizum payment method
        /// </summary>
        public IList<string> Flags { get; set; }

        /// <summary>
        /// The initialization status or token for Bizum
        /// </summary>
        public string Initialization { get; set; }

        /// <summary>
        /// Payment method options specific to Bizum
        /// </summary>
        public BizumOptions PaymentMethodOptions { get; set; }
    }
}
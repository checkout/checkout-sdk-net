using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentMethods
    {
        /// <summary>
        /// Klarna payment method configuration
        /// </summary>
        public Klarna Klarna { get; set; }

        /// <summary>
        /// STC Pay payment method configuration
        /// </summary>
        public Stcpay Stcpay { get; set; }

        /// <summary>
        /// Tabby payment method configuration
        /// </summary>
        public Tabby Tabby { get; set; }

        /// <summary>
        /// Bizum payment method configuration
        /// </summary>
        public Bizum Bizum { get; set; }
    }
}